using Assets.Scripts.Player;
using UnityEngine;

public class ReverseSectionEngager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
           
            //Make sure the track doesnt keep restarting when the player hits this hidden wall
            if (player.isReversing != true)
            {
                AudioManager.GetInstance().StopMusic();
                AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.reversalMusic);
            }
            
            player.isReversing = true;
        }
    }
}