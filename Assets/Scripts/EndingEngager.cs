using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingEngager : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            // Make sure the reversal music stops when player reaches the end
            if (gameManager.isReversing)
            {
                AudioManager.GetInstance().StopMusic();
                AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.exploreMusic);
            }

            gameManager.isReversing = false;
        }
    }
}
