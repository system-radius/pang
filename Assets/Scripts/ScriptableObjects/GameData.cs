using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public float value;
    public float defaultValue;

    public void Reset()
    {
        value = defaultValue;
    }
}
