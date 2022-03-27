using UnityEngine;

/// <summary>
/// A component that is basically a game observer.
/// Observes the amount of the balls remaining in the game.
/// Also observes if the player still exists in the game.
/// Will advance the level if there are no more balls detected.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerInfo mainPlayerInfo = null;

    [SerializeField] private PlayerData respawnTimeLimit = null;

    [SerializeField] private GameData level = null;

    [SerializeField] private GameData gameTimeLimit = null;

    [SerializeField] private GameEvent gameOverEvent = null;

    [SerializeField] private GameEvent spawnPlayerEvent = null;

    [SerializeField] private GameEvent nextLevelEvent = null;

    [SerializeField] private GameObject ballPrefab = null;

    [SerializeField] private GameObject container = null;

    [SerializeField] private GameObject[] ballSpawnPoints = null;

    private bool gameOver = false;

    private bool alive = false;

    void Start()
    {
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

    public void AdvanceLevel()
    {
        level.value++;
        Restart();
    }

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

    public void SpawnPlayerSuccess()
    {
        alive = true;
    }
}
