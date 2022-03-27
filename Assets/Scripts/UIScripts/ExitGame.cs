using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple script to exit the game.
/// </summary>
public class ExitGame : MonoBehaviour
{
    /// <summary>
    /// This method is called on raise of the exit game event.
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }
}
