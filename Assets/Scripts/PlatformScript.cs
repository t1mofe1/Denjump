using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public bool isFixed = false;
    public float speed = 3f;
    public float delay = 0.5f;

    private Vector3 desiredPosition;
    private bool forwardTurn = true;
    private bool moving = false;

    private float platformWidth;

    // TODO: Implement right boundaries to move between. Maybe take from player pos?

    private void Start()
    {
        platformWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;

        StartMoving();
    }

    private void StartMoving()
    {
        if (moving) return;

        float x = forwardTurn ? transform.position.x + 3.8f * 2 : transform.position.x - 3.8f * 2;

        Vector3 newPosition = new Vector3(x, transform.position.y, transform.position.z);
        desiredPosition = newPosition;

        moving = true;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);
        }

        if (Vector3.Distance(transform.position, desiredPosition) < 0.02)
        {
            moving = false;
            forwardTurn = !forwardTurn;
            Invoke("StartMoving", delay);
        }
    }
}
