using UnityEngine;

public class PlayerPlatformCollision : MonoBehaviour
{
    public GameObject Platforms;
    private PlatformSpawner script;

    public bool isLanded;

    private void Start()
    {
        script = Platforms.GetComponent<PlatformSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isLanded = true;
        script.OnPlayerLand(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isLanded = false;
    }
}
