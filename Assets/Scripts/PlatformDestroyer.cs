using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public GameObject player;

    public float offset = 1f;

    private void OnBecameInvisible()
    {
        Debug.Log("Invisible");

        if (gameObject.transform.up.y + offset < player.transform.up.y)
        {
            //Destroy(gameObject);
        }
    }
}
