using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    private GameObject playerObj;
    private PlatformSpawner platformSpawnerScript;
    private PlatformMovement platformMovementScript;
    private UserMovements userMovementsScript;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        platformMovementScript = GetComponent<PlatformMovement>();
        platformSpawnerScript = GetComponentInParent<PlatformSpawner>();
        userMovementsScript = playerObj.GetComponent<UserMovements>();

        // Check if some of the variables are not present
        Debug.Assert(playerObj, $"PlayerObj not found in PlatformCollision.. Check if object with tag 'Player' exists");
        Debug.Assert(platformMovementScript, $"PlatformMovementScript not found in PlatformCollision.. Check if this script is in this platform object");
        Debug.Assert(platformSpawnerScript, $"PlatformSpawnerScript not found in PlatformCollision.. Check if this script is in 'Platforms' object");
        Debug.Assert(userMovementsScript, $"UserMovementsScript not found in PlatformCollision.. Check if this script is in 'Player' object");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If platform collided with sth else than player
        if (collision.gameObject.tag != "Player") return;

        // If platform collided not from the top
        float collisionY = collision.GetContact(0).point.y;
        if (collisionY > playerObj.transform.position.y) return;

        userMovementsScript.isLanded = true;

        platformMovementScript.MakePlatformFixed();

        platformSpawnerScript.OnUserLand(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If platform collided with sth else than player
        if (collision.gameObject.tag != "Player") return;

        userMovementsScript.isLanded = false;
    }
}
