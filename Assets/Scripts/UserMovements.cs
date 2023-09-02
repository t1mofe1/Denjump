using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public struct JumpEventProps
{
    public int jumpsUsed;
    public int maxJumps;

    public bool isLastJump
    {
        get { return jumpsUsed == maxJumps; }
    }
}

public class UserMovements : MonoBehaviour
{
    public InputActionAsset actions;
    private Rigidbody2D rb;
    public AudioSource jumpSound;

    public float jumpForce = 2f;
    public float dblJumpForce = 1f;
    public int maxJumps = 2;

    [HideInInspector]
    public bool isLanded;

    [HideInInspector]
    public int jumpsUsed = 0;

    private UnityEvent<JumpEventProps> jumpEvent = new UnityEvent<JumpEventProps>();

    void Start()
    {
        isLanded = true;
        rb = GetComponent<Rigidbody2D>();

        // Check if some of the variables are not present
        Debug.Assert(jumpSound, $"JumpSound not found in UserMovements.. Check if it's provided to the script");
        Debug.Assert(actions, $"Actions not found in UserMovements.. Check if it's provided to the script");
        Debug.Assert(rb, $"Rb not found in UserMovements.. Check if RigidBody2D is on the 'Player' object");

        // Jump action
        InputAction jumpAction = actions.FindAction("Jump");
        jumpAction.performed += ctx => OnJump();
        jumpAction.Enable();
    }

    private void FixedUpdate()
    {
        if (isLanded) jumpsUsed = 0;
    }

    private void OnJump()
    {
        // Update jumps used
        jumpsUsed++;

        // If player is not landed yet
        if (!isLanded && jumpsUsed >= maxJumps) { return; }

        // Calculate force based on jumps used
        float force = jumpsUsed == 0 ? jumpForce : dblJumpForce;

        rb.velocity = new Vector2(0, force);

        jumpSound.Play();

        // Invoke custom callbacks
        jumpEvent.Invoke(new JumpEventProps { jumpsUsed = jumpsUsed, maxJumps = maxJumps });
    }

    // #region Jump Event
    public void AddJumpCallback(UnityAction<JumpEventProps> cb)
    {
        jumpEvent.AddListener(cb);
    }

    public void RemoveJumpCallback(UnityAction<JumpEventProps> cb)
    {
        jumpEvent.RemoveListener(cb);
    }
    // #endregion Jump Event
}
