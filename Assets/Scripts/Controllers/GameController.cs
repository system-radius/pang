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

    [SerializeField] private GameData gameTimeLimit = null;

    [SerializeField] private GameEvent gameOverEvent = null;

    [SerializeField] private GameEvent spawnPlayerEvent = null;

    private bool gameOver = false;

    private bool alive = false;

    void Start()
    {
        mainPlayerInfo.Reset();
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

        if (respawnTimeLimit.value <= 1.0001 && !alive)
        {
            // Respawn the player.
            spawnPlayerEvent.Raise();
            //Debug.Log("Raise spawn event!");
        }
    }

    private void Restart()
    {
        // Reset the time limit for the level
        gameTimeLimit.Reset();

        // Call to respawn a player instance.
        alive = false;
        respawnTimeLimit.value = 0;
    }

    public void KillPlayer()
    {
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
