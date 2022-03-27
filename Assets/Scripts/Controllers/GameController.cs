using UnityEngine;

/// <summary>
/// A component that is basically a game observer.
/// Observes the amount of the balls remaining in the game.
/// Also observes if the player still exists in the game.
/// Will advance the level if there are no more balls detected.
/// </summary>
public class GameController : MonoBehaviour
{
    // Contains grouped player information.
    [SerializeField] private PlayerInfo mainPlayerInfo = null;

    // The time limit before calling to respawn the player.
    [SerializeField] private PlayerData respawnTimeLimit = null;

    // Data containing the current level.
    [SerializeField] private GameData level = null;

    // The time limit to clear the level before forcing a game over.
    [SerializeField] private GameData gameTimeLimit = null;

    // The event to be raised when reaching a game over state.
    [SerializeField] private GameEvent gameOverEvent = null;

    // The event to be raised when the player should be spawned.
    [SerializeField] private GameEvent spawnPlayerEvent = null;

    // The event to be raised when the level is cleared.
    [SerializeField] private GameEvent nextLevelEvent = null;

    // The ball instance to be spawned.
    [SerializeField] private GameObject ballPrefab = null;

    // The container of all the balls to be spawned.
    [SerializeField] private GameObject container = null;

    // The possible spawn points of the balls.
    [SerializeField] private GameObject[] ballSpawnPoints = null;

    private bool gameOver = false;

    // Player status, necessary to avoid redundant spawns.
    private bool alive = false;

    void Start()
    {
        // Reset some of the data to their original value.
        mainPlayerInfo.Reset();
        level.Reset();
        Restart();
    }

    void Update()
    {
        // Nothing more to do.
        if (gameOver) return;

        gameTimeLimit.value -= Time.deltaTime;
        if (gameTimeLimit.value <= 0.0001)
        {
            // Instant game over for the player if the time limit is reached.
            gameOver = true;
            gameOverEvent.Raise();
            return;
        }

        if (respawnTimeLimit.value > 0)
        {
            // Count down the respawn timer.
            respawnTimeLimit.value -= Time.deltaTime;
            // The player is dead, there is no need for further checking.
            return;
        }

        if (respawnTimeLimit.value <= 0.0001 && !alive)
        {
            // Respawn the player.
            spawnPlayerEvent.Raise();
            //Debug.Log("Raise spawn event!");
        }

        // Check the existence of the balls.
        if (!DetectBalls())
        {
            // There are no more balls in the level, and may proceed.
            //Debug.Log("Level Complete!");
            nextLevelEvent.Raise();
        }
    }

    /// <summary>
    /// Called when the NextLevelProceed event has been raised.
    /// </summary>
    public void AdvanceLevel()
    {
        // Move to the next level.
        level.value++;
        Restart();
    }

    /// <summary>
    /// Called at every start of the game/level.
    /// This ensures that a player instance will be spawned right away,
    /// as well as the balls.
    /// </summary>
    private void Restart()
    {
        // Reset the time limit for the level
        gameTimeLimit.Reset();

        // Call to respawn a player instance.
        alive = false;
        respawnTimeLimit.value = 0;

        // Spawn the balls for the current level.
        float initialForce = 2.5f;
        for (int i = 0; i < level.value; i++)
        {
            Transform ball = Instantiate(ballPrefab, ballSpawnPoints[i].transform.position, Quaternion.identity, container.transform).transform;
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(initialForce, 0);
            initialForce = -initialForce;
        }
    }

    /// <summary>
    /// Function to check the existence of the balls
    /// in the playing field.
    /// </summary>
    /// <returns>true if there exists at least 1 ball, false otherwise.</returns>
    private bool DetectBalls()
    {
        bool hasBalls = false;

        foreach (Transform child in container.transform)
        {
            if (child != null)
            {
                if (child.position.y < -4)
                {
                    Destroy(child.gameObject);
                    continue;
                }
                hasBalls = true;
            }
        }

        return hasBalls;
    }

    /// <summary>
    /// "Kills" the player for this controller. This starts
    /// the countdown for the respawn of the player, or if the player
    /// has no more lives left, raises the game over event.
    /// Called when the player killed event has been raised.
    /// </summary>
    public void KillPlayer()
    {
        // No need to process if player is already dead.
        if (!alive) return;

        mainPlayerInfo.lives.value--;
        if (mainPlayerInfo.lives.value < 0)
        {
            // Reset the life value to 0 for display purposes.
            mainPlayerInfo.lives.value = 0;
            gameOver = true;
            gameOverEvent.Raise();
            return;
        }

        alive = false;
        respawnTimeLimit.Reset();
    }

    /// <summary>
    /// Called when the player spawn success event has been raised.
    /// Prevents the redundant spawning of the player.
    /// </summary>
    public void SpawnPlayerSuccess()
    {
        alive = true;
    }
}
