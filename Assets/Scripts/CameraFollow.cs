using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;
    public float speed = 3f;
    public float offset = 2f;

    private Transform GameMinHeight;

    private void Start()
    {
        GameMinHeight = GameObject.Find("GameMinHeight").transform;

        // Check if some of the variables are not present
        Debug.Assert(GameMinHeight, $"GameMinHeight not found in CameraFollow.. Check if object with name 'GameMinHeight' exists");
        Debug.Assert(followTarget, $"FollowTarget not found in CameraFollow.. Check if you set the object in inspector");
    }

    void Update()
    {
        Vector3 followTargetPos = new Vector3(transform.position.x, followTarget.transform.position.y + offset, transform.position.z);
        Vector3 cameraPos = Vector3.Max(followTargetPos, GameMinHeight.position);

        transform.position = Vector3.Lerp(transform.position, cameraPos, speed * Time.deltaTime);
    }
}
