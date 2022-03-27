using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A UI script that contains a collection of methods
/// that are triggered from within the game scene.
/// These methods do not make the game
/// move from one scene to another.
/// </summary>
public class GameFlow : MonoBehaviour
{
    [SerializeField] private LeaderboardAccess access = null;
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
    }

    public void SaveScore()
    {
        access.SaveScore();
    }
}
