using UnityEngine;
using UnityEngine.InputSystem;

public class UserMovements : MonoBehaviour
{
    public InputActionAsset actions;
    private Rigidbody2D rb;

    public float jumpForce = 20f;
    public float dblJumpForce = 10f;
    public int maxJumps = 2;

    [HideInInspector]
    public bool isLanded;

    [HideInInspector]
    public int jumpsUsed = 0;

    void Start()
    {
        isLanded = true;
        rb = GetComponent<Rigidbody2D>();

        // Check if some of the variables are not present
        Debug.Assert(actions, $"Actions not found in UserMovements.. Check if it's provided to the script");
        Debug.Assert(rb, $"Rb not found in UserMovements.. Check if RigidBody2D is on the 'Player' object");

        // Jump action
        InputAction jumpAction = actions.FindAction("Jump");
        jumpAction.performed += ctx => OnJump();
        jumpAction.Enable();
    }

    private void Update()
    {
        if (isLanded) jumpsUsed = 0;
    }

    void OnJump()
    {
        // If player is not landed yet
        if (!isLanded && jumpsUsed >= maxJumps) { return; }

        float force = jumpsUsed == 0 ? jumpForce : dblJumpForce;

        rb.AddForce(new Vector2(0, force));
        jumpsUsed++;
    }
}
