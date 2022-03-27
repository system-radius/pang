﻿using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    public PlayerData lives;
    public PlayerData movementSpeed;
    public PlayerData score;

    public bool firing;

    public PlayerState state;

    public void Reset()
    {
        lives.Reset();
        score.Reset();
    }
}
