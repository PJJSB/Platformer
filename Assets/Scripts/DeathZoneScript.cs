using Assets.Scripts.Player;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    public GameObject respawnAnchor;
    public GameObject respawnAnchorReturn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            AudioManager.GetInstance().Play(AudioManager.SoundType.death);

            // The jank is real, controller needs to be disabled to be able to pass through objects to a respawn anchor
            controller.enabled = false;

            // Which side of the room the player will respawn at is based on how the player is traversing the level (front -> back or back -> front) at that point
            if (!player.isReversing)
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