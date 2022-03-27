using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A UI script that primarily handles the availability
/// of the mobile controls. Since the joystick/button
/// might hinder the player's view when playing on other
/// platforms, they are disabled by default.
/// </summary>
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
            // Enable the mobile controls.
            mobileControls.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Exposed method to disable the controls. There are
    /// some instances when the controls are not needed
    /// even while playing on mobile.
    /// </summary>
    public void DisableMobileControls()
    {
        mobileControls.gameObject.SetActive(false);
    }
}
