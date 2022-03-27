using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component that will spawn the player on the field.
/// Will raise the game over event when the player has no lives left.
/// </summary>
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private PlayerData invulTime = null;

    [SerializeField] private GameEvent spawnSuccess = null;

    [SerializeField] private GameObject playerPrefab = null;

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

        playerInstance.tag = "Player";
        playerInstance.GetComponent<SpriteRenderer>().color = Color.white;
        spawnComplete = true;
    }

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

    public void KillPlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
    }
}
