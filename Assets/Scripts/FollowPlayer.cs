using UnityEngine;

namespace Assets.Scripts
{
    public class FollowPlayer : MonoBehaviour
    {
        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            // Check if some of the variables are not present
            Debug.Assert(player, $"Player not found in FollowPlayer.. Check if object with tag 'Player' exists");
        }


        void FixedUpdate()
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}