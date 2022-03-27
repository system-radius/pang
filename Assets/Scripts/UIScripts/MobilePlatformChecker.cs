using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatformChecker : MonoBehaviour
{
    [SerializeField] private RectTransform mobileControls = null;

    void Awake()
    {
        EnableMobileControls();
    }

    public void EnableMobileControls()
    {
        if (Application.isMobilePlatform)
        {
            mobileControls.gameObject.SetActive(true);
        }
    }

    public void DisableMobileControls()
    {
        mobileControls.gameObject.SetActive(false);
    }
}
