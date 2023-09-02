using UnityEngine;
using UnityEngine.Events;

public class PlatformSpawner : MonoBehaviour
{
    private GameObject Player;
    private GameObject PlatformPrefab;

    private Transform spawnPos;

    public float spawnHeightAbovePlayer = 1f;

    private GameObject lastPlatform = null;

    private UnityEvent<Collision2D> userLandEvent = new UnityEvent<Collision2D>();

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlatformPrefab = GameObject.Find("PlatformPrefab");
        spawnPos = GameObject.Find("StartPos").transform;

        // Check if some of the variables are not present
        Debug.Assert(Player, $"Player not found in PlatformSpawner.. Check if object with tag 'Player' exists");
        Debug.Assert(PlatformPrefab, $"PlatformPrefab not found in PlatformSpawner.. Check if you set the object in inspector");
        Debug.Assert(spawnPos, $"SpawnPos not found in PlatformSpawner.. Check if you set the transform in inspector");

        CreatePlatform();
    }

    void CreatePlatform()
    {
        // Create position for new platform
        Vector3 platformSpawnPos = new Vector3(spawnPos.position.x, Player.transform.position.y + spawnHeightAbovePlayer, spawnPos.position.z);

        // Create new platform
        GameObject platform = Instantiate(PlatformPrefab, platformSpawnPos, Quaternion.identity, transform);

        // Change platform name for checking 'lastPlatformName'
        platform.name = string.Concat("Platform-", GeneratePlatformName());

        // Get platform collision script
        PlatformCollision platformCollisionScript = platform.GetComponent<PlatformCollision>();

        // Check if the script is present
        Debug.Assert(platformCollisionScript, $"PlatformCollisionScript not found in Platform.. Check if script exists on PlatformPrefab");
    }

    public void OnUserLand(Collision2D collision)
    {
        GameObject platform = collision.otherCollider.gameObject;

        // If platform is not the first one
        if (lastPlatform != null)
        {
            // If player landed on the same platform
            if (platform.name == lastPlatform.name) return;
            // If player landed on the same or some previous platforms down there
            else if (platform.transform.position.y <= lastPlatform.transform.position.y) return;
        }

        userLandEvent.Invoke(collision);

        // Save last platform to check next time
        lastPlatform = platform;

        // Create new platform
        CreatePlatform();
    }

    // #region User Land Callback
    public void AddUserLandCallback(UnityAction<Collision2D> cb)
    {
        userLandEvent.AddListener(cb);
    }

    public void RemoveUserLandCallback(UnityAction<Collision2D> cb)
    {
        userLandEvent.RemoveListener(cb);
    }
    // #endregion User Land Callback

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
