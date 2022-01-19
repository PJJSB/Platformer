using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject respawnAnchor;
    public GameObject respawnAnchorReturn;
    public Animator deathTransition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            StartCoroutine(StartDeathTransition(other));
        }
    }

    private IEnumerator StartDeathTransition(Collider other)
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.death);

        deathTransition.SetBool("hasDied", true);
        gameManager.isRespawning = true;
        // Pause game to prevent player from walking
        GameManager.isPaused = true;

        yield return new WaitForSeconds(1);
        
        CharacterController controller = other.GetComponent<CharacterController>();

        // The jank is real, controller needs to be disabled to be able to pass through objects to a respawn anchor
        controller.enabled = false;

        // Which side of the room the player will respawn at is based on how the player is traversing the level (front -> back or back -> front) at that point
        if (!gameManager.isReversing)
        {
            other.transform.position = respawnAnchor.transform.position;
        }
        else
        {
            other.transform.position = respawnAnchorReturn.transform.position;
            // Reset the reversal when a player dies during the reversal
            GameManager.GetInstance().ResetReversal();
        }

        controller.enabled = true;

        // Increment deathcount
        other.GetComponent<PlayerStats>().deathCount++;

        deathTransition.SetBool("hasDied", false);
        gameManager.isRespawning = false;
        GameManager.isPaused = false;
    }

}