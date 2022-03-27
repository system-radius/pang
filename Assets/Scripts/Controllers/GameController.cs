using UnityEngine;

/// <summary>
/// A component that is basically a game observer.
/// Observes the amount of the balls remaining in the game.
/// Also observes if the player still exists in the game.
/// Will advance the level if there are no more balls detected.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private GameData gameTimeLimit = null;

    [SerializeField] private GameEvent gameOverEvent = null;

    private bool gameOver = false;

    void Start()
    {
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
    }

    private void Restart()
    {
        gameTimeLimit.Reset();
    }
}
