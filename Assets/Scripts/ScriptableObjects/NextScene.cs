using UnityEngine;

/// <summary>
/// Scene ID container that can be set to various objects.
/// Mostly used with the NextSceneLoader component.
/// </summary>
[CreateAssetMenu]
public class NextScene : ScriptableObject
{
    public int sceneId = 0;
}
