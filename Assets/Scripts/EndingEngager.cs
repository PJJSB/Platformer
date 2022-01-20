using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingEngager : MonoBehaviour
{
    public GameManager gameManager;
    public Animator endScreen;
    public GameObject endScreenPanel;
    public TextMeshProUGUI finalDeaths;
    public TextMeshProUGUI finalTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            // Make sure the reversal music stops when player reaches the end
            if (gameManager.isReversing)
            {
                //Show cursor
                Cursor.lockState = CursorLockMode.None;

                finalDeaths.text = "Deaths: " + gameManager.playerStats.deathCount;
                finalTime.text = "Playtime: " + gameManager.playerStats.ReturnTime();

                GameManager.isEnding = true;
                Time.timeScale = 0f;
                GameManager.isPaused = true;

                AudioManager.GetInstance().StopMusic();
                AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.introMusic);

                endScreenPanel.SetActive(true);
                endScreen.SetTrigger("StartEndScreen");
            }

            gameManager.isReversing = false;
        }
    }
}
