using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component that will spawn the player on the field.
/// Will raise the game over event when the player has no lives left.
/// </summary>
public class PlayerSpawner : MonoBehaviour
{
    // The amount of time that the player cannot be hit.
    [SerializeField] private PlayerData invulTime = null;

    // The event to be triggered on successful spawning of the player.
    [SerializeField] private GameEvent spawnSuccess = null;

    // The player object to be instantiated.
    [SerializeField] private GameObject playerPrefab = null;

    // The point where the player will be spawned.
    [SerializeField] private GameObject spawnPoint = null;

    private GameObject playerInstance;

    private bool spawnComplete = false;

    void Update()
    {
        if (spawnComplete) return;

        if (invulTime.value > 0.0001)
        {
            invulTime.value -= Time.deltaTime;
            return;
        }

        // Set the tag of the instance to player, allowing it
        // to trigger collision events with balls.
        playerInstance.tag = "Player";

        // Also remove the tint to signify that the invulnerability
        // has been removed.
        playerInstance.GetComponent<SpriteRenderer>().color = Color.white;

        // Flip the switch, so that no more settings will be done for now.
        spawnComplete = true;
    }

    /// <summary>
    /// Called when the player spawn event is raised.
    /// </summary>
    public void SpawnPlayer()
    {

        Vector3 spawnLocation = Vector3.zero;
        if (spawnPoint != null)
        {
            spawnLocation = spawnPoint.transform.position;
        }

        // Spawn the player.
        playerInstance = Instantiate(playerPrefab, spawnLocation, Quaternion.identity);

        // On spawn, set the player as untagged to avoid collision with the balls.
        playerInstance.tag = "Untagged";
        playerInstance.GetComponent<SpriteRenderer>().color = Color.cyan;
        invulTime.Reset();

        // Mark the current spawn as incomplete because of the invulnerability.
        spawnComplete = false;

        // Trigger the spawn success events.
        spawnSuccess.Raise();
    }

    /// <summary>
    /// As this component contains the direct reference to the player,
    /// it is also used to kill the said instance.
    /// Called when the player killed event is raised.
    /// </summary>
    public void KillPlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
    }
}
