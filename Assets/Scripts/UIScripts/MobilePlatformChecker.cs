using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatformChecker : MonoBehaviour
{
    [SerializeField] private RectTransform mobileControls = null;

    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            mobileControls.gameObject.SetActive(true);
        }
    }
}
