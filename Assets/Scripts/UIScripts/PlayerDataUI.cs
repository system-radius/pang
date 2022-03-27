using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A UI script that intends to display various player data
/// to text fields.
/// </summary>
public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI visualText = null;
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private float offset = 0;
    [SerializeField] private bool truncate = false;

    void Update()
    {
        if (visualText == null) return;

        float value = (float)playerData.value + offset;
        if (truncate)
        {
            value = (int)value;
        }
        visualText.text = value.ToString();
    }

}
