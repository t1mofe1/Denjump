using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;
    public float speed = 2f;

    private float differenceFromTarget;

    private void Start()
    {
        differenceFromTarget = transform.position.x - followTarget.transform.position.x;
    }

    void Update()
    {
        Vector3 targetPosition = new Vector3(followTarget.transform.position.x + differenceFromTarget, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
