using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlatformPrefab;

    private string lastPlatformName;

    private void Start()
    {
        //CreatePlatform();
    }

    public void OnPlayerLand(Collision2D collision)
    {
        if(collision.gameObject.name == lastPlatformName)
        {
            Debug.Log("Landed on the same platform");
            return;
        }

        lastPlatformName = collision.gameObject.name;

        CreatePlatform();
    }

    void CreatePlatform()
    {
        Debug.Log("Creating new platform");

        Vector3 playerPos = Player.transform.position;
        float platformWidth = PlatformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        float platformPosX = playerPos.x - 3.5f;
        Vector3 platformPos = new Vector3(platformPosX, playerPos.y + 1.25f, playerPos.z);

        GameObject platform = Instantiate(PlatformPrefab, platformPos, Quaternion.identity, transform);
        platform.name = string.Concat("Platform-", GeneratePlatformName());
    }

    private string GeneratePlatformName(int length = 5)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[Random.Range(0, chars.Length)];
        }

        string finalString = new string(stringChars);

        return finalString;
    }
}
