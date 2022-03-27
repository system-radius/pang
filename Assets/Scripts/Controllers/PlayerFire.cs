using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : PlayerAction
{
    // Bullet prefab to be created when doing the firing action.
    [SerializeField] private GameObject bulletPrefab;

    // The time limit between firing actions.
    [SerializeField] private float firingTimeLimit = 0.1f;

    // The maximum number of bullets that can be fired in succession.
    [SerializeField] private int maxBullets = 1;

    // A collection of all the bullets fired that are still active.
    private List<Transform> bullets;

    // Whether the player is firing or not.
    private bool firing;

    // The amount of time since the firing first triggered.
    private float firingTimer;

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Always set the firing status in the player data.
        playerData.firing = firing;

        // If currently firing, count down towards the time when it is allowed again.
        if (firing)
        {
            firingTimer -= Time.deltaTime;
            firing = firingTimer > 0f;
            return;
        }

        ClearBullets();

    }

    /// <summary>
    /// Removes the null bullet instances in the list.
    /// Helps in tracking of the amount that the player can fire.
    /// </summary>
    private void ClearBullets()
    {
        for (var i = bullets.Count - 1; i >= 0; i--)
        {
            if (bullets[i] == null) bullets.RemoveAt(i);
        }
    }

    /// <summary>
    /// Method to be registered on the PlayerInput component.
    /// </summary>
    /// <param name="context"></param>
    public void OnFire(InputAction.CallbackContext context)
    {
        // If already firing, return.
        if (firing) return;

        firing = context.action.triggered;

        if (firing)
        {
            // Ensure that the variable is set to true.
            // This function may have been called to set the variable to false.
            firingTimer = firingTimeLimit;

            if (bullets.Count < maxBullets)
            {
                bullets.Add(Instantiate(bulletPrefab, transform.position, Quaternion.identity).transform);
            }
        }
    }
}
