using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the control definition for the action of the player
/// retricted by the current state. If the state is not compatible
/// with the action, the action will not proceed.
/// </summary>
public class PlayerAction : MonoBehaviour
{
    // Contains the current data of the player, may be relevant to subclasses.
    [SerializeField] protected PlayerInfo playerData;

    // Contains all the states where this action can take over.
    [SerializeField] private PlayerState[] allowedStates = null;

    protected bool CanAct()
    {
        bool canAct = false;

        foreach (PlayerState state in allowedStates)
        {
            if (state == playerData.state)
            {
                canAct = true;
                break;
            }
        }

        return canAct;
    }
}
