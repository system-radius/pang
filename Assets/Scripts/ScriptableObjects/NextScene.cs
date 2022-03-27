using UnityEngine;

/// <summary>
/// Scene ID container so that they can be created
/// and set to various objects.
/// </summary>
[CreateAssetMenu]
public class NextScene : ScriptableObject
{
    public int sceneId = 0;
}
