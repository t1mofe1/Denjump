using UnityEngine;
using UnityEngine.InputSystem;

public class UserMovements : MonoBehaviour
{
    public InputActionAsset actions;
    private Rigidbody2D rb;

    public float jumpForce = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        InputAction jumpAction = actions.FindAction("Jump");

        jumpAction.performed += ctx => OnJump();
        jumpAction.Enable();
    }

    void OnJump()
    {
        if (!gameObject.GetComponent<PlayerPlatformCollision>().isLanded)
        {
            return;
        }

        rb.AddForce(new Vector2(0, jumpForce));
    }
}
