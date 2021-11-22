using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    public GameObject respawnAnchor;
    public GameObject respawnAnchorReturn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            var controller = other.GetComponent<CharacterController>();
            var player = other.GetComponent<PlayerStateManager>();

            AudioManager.GetInstance().PlayDeath();

            // the jank is real
            controller.enabled = false;

            // Which side of the room the player will respawn at is based on how the player is traversing the level (front -> back or back -> front) at that point
            if (!player.isReverseSection)
            {
                other.transform.position = respawnAnchor.transform.position;
            }
            else
            {
                other.transform.position = respawnAnchorReturn.transform.position;
            }
            
            controller.enabled = true;
        }
    }
}
