using UnityEngine;
using UnityEngine.UI;

public class PlatformSpawner : MonoBehaviour
{
    private GameObject Player;
    private GameObject PlatformPrefab;

    private Transform spawnPos;

    public float spawnHeightAbovePlayer = 1f;

    private GameObject lastPlatform = null;

    //score 
    public int count;
    public Text text;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlatformPrefab = GameObject.Find("PlatformPrefab");
        spawnPos = GameObject.Find("StartPos").transform;

        // Check if some of the variables are not present
        Debug.Assert(Player, $"Player not found in PlatformSpawner.. Check if object with tag 'Player' exists");
        Debug.Assert(PlatformPrefab, $"PlatformPrefab not found in PlatformSpawner.. Check if you set the object in inspector");
        Debug.Assert(spawnPos, $"SpawnPos not found in PlatformSpawner.. Check if you set the transform in inspector");
    }

    public void OnPlayerLand(Collision2D collision)
    {
        GameObject platform = collision.otherCollider.gameObject;

        if (lastPlatform != null)
        {
            // If player landed on the same platform
            if (platform.name == lastPlatform.name)
            {
                Debug.Log("Landed on the same platform");
                return;
            }
            // If player landed on the same or some previous platforms down there
            else if (platform.transform.position.y <= lastPlatform.transform.position.y)
            {
                Debug.Log("Landed on same or some of the previous platforms");
                return;
            }
        }

        // Save last platform to check next time
        lastPlatform = platform;

        // Create new platform
        CreatePlatform();
    }

    void CreatePlatform()
    {
        // Create position for new platform
        Vector3 platformSpawnPos = new Vector3(spawnPos.position.x, Player.transform.position.y + spawnHeightAbovePlayer, spawnPos.position.z);

        // Create new platform
        GameObject platform = Instantiate(PlatformPrefab, platformSpawnPos, Quaternion.identity, transform);

        //platform.transform.position = platformSpawnPos;

        // Change platform name for checking 'lastPlatformName'
        platform.name = string.Concat("Platform-", GeneratePlatformName());

        //Проверка счета
        count++;
        text.text = count.ToString();
        Debug.Log(count);
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
