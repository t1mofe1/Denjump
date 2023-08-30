using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;
    public float speed = 3f;
    public float offset = 2f;

    void Update()
    {
        Vector3 targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y + offset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
