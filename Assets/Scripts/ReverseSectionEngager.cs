using Assets.Scripts.Player;
using UnityEngine;

public class ReverseSectionEngager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.isReversing = true;
            AudioManager.GetInstance().StopMusic();
            AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.reversalMusic);
        }
    }
}