using UnityEngine;
using TMPro;

/// <summary>
/// A UI script that intends to display various game data
/// to text fields.
/// </summary>
public class GameDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI visualText = null;
    [SerializeField] private GameData gameData = null;
    [SerializeField] private bool truncate = false;

    void Update()
    {
        if (visualText == null) return;

        float value = gameData.value;
        if (truncate)
        {
            value = (int)value;
        }
        visualText.text = value.ToString();
    }
}
