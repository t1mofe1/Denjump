using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [HideInInspector]
    public bool isFixed = false;
    public float baseSpeed = 1f;
    public float delay = 0f;

    // TODO: Each platform should have higher speed than previous one
    private float speed = 1f;

    private Transform startPos;
    private Transform endPos;

    private bool forwardTurn = true;

    private void Start()
    {
        isFixed = gameObject.name == "Platform";

        speed = baseSpeed;

        startPos = GameObject.Find("StartPos").transform;
        endPos = GameObject.Find("EndPos").transform;

        // Check if some of the variables are not present
        Debug.Assert(startPos, $"StartPos not found in PlatformMovement.. Check if object with name 'StartPos' exists");
        Debug.Assert(endPos, $"EndPos not found in PlatformMovement.. Check if object with name 'EndPos' exists");
    }

    private void FixedUpdate()
    {
        if (isFixed) return;

        float targetX = forwardTurn ? endPos.position.x : startPos.position.x;

        Vector2 target = new Vector2(targetX, transform.position.y);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            isFixed = true;

            Invoke(nameof(StartMoving), delay);

            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void MakePlatformFixed()
    {
        isFixed = true;
    }

    private void StartMoving()
    {
        isFixed = false;
        forwardTurn = !forwardTurn;
    }
}
