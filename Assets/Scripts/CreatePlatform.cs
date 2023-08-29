using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatform : MonoBehaviour
{
    public GameObject platformPrefab;

    void Start()
    {
        InvokeRepeating("CreateNewPlatform", 0f, 1f);
    }

    void CreateNewPlatform()
    {
        Vector3 position = new Vector3(transform.position.x, Random.Range(-2f, 2f), transform.position.z);

        
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity, transform);

        platform.name = "Platform";
    }
}
