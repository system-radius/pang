using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component that will spawn the player on the field.
/// Will raise the game over event when the player has no lives left.
/// </summary>
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameEvent spawnSuccess = null;

    [SerializeField] private GameObject playerPrefab = null;

    [SerializeField] private GameObject spawnPoint = null;

    private GameObject playerInstance;

    public void SpawnPlayer()
    {

        Vector3 spawnLocation = Vector3.zero;
        if (spawnPoint != null)
        {
            spawnLocation = spawnPoint.transform.position;
        }

        // Spawn the player.
        playerInstance = Instantiate(playerPrefab, spawnLocation, Quaternion.identity);
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
