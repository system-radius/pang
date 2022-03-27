using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public double value;
    public float defaultValue = 0;

    public void Reset()
    {
        value = defaultValue;
    }
}
