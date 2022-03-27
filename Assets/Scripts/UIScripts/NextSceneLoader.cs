using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A script component that will allow transition from
/// one scene to another.
/// </summary>
public class NextSceneLoader : MonoBehaviour
{
    public NextScene scene;

    public void LoadNextScene()
    {
        if (scene == null)
        {
            return;
        }
        SceneManager.LoadScene(scene.sceneId);
    }
}
