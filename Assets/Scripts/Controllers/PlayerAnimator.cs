using UnityEngine;

/// <summary>
/// Responsible for updating the animations for the player character.
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerData = null;

    // The possible states of the player.
    [SerializeField] private PlayerState idleState = null;
    [SerializeField] private PlayerState movementState = null;
    [SerializeField] private PlayerState firingState = null;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale <= 0) return;

        // Always set the player back to idle, it will be overridden by other states.
        playerData.state = idleState;

        float ms = (float)playerData.movementSpeed.value;
        animator.SetFloat("Speed", ms);
        playerData.state = ms > 0 ? movementState : playerData.state;

        // The firing state can override the movement state.
        animator.SetBool("Firing", playerData.firing);
        playerData.state = playerData.firing ? firingState : playerData.state;
    }
}
