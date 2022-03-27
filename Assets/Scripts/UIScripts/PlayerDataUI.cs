using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI visualText = null;
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private bool truncate = false;

    void Update()
    {
        if (visualText == null) return;

        float value = (float)playerData.value;
        if (truncate)
        {
            value = (int)value;
        }
        visualText.text = value.ToString();
    }

}
