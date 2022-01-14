using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public GameObject pauseMenu;
    public bool isPaused;
    
    public GameObject hubRespawnAnchor;
    public PlayerMovement player;
    public PlayerStats playerStats;

    private TextMeshProUGUI _playTime;
    private TextMeshProUGUI _deaths;

    private void Start()
    {
        //Set pause menu to deactive as default
        pauseMenu.SetActive(false);

        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;

        _playTime = pauseMenu.transform.Find("txt_Playtime").GetComponent<TextMeshProUGUI>();
        _deaths = pauseMenu.transform.Find("txt_Deaths").GetComponent<TextMeshProUGUI>();


        //Play explore music
        AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.exploreMusic);
    }

    private void Update()
    {
        if (player.playerInput.actions["Pause"].triggered)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        //Show cursor
        Cursor.lockState = CursorLockMode.None;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        
        // Update playtime and deaths in pause menu
        _playTime.text = "Playtime: " + playerStats.ReturnTime();
        _deaths.text = "Deaths: " + playerStats.deathCount;
    }

    public void ResumeGame()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);

        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;

        pauseMenu?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToHub()
    {
        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;

        pauseMenu?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);

        var controller = player.controller;

        // The jank is real, controller needs to be disabled to be able to pass through objects to a respawn anchor
        controller.enabled = false;

        var playerTransform = player.transform;
        playerTransform.position = hubRespawnAnchor.transform.position;
        playerTransform.eulerAngles = new Vector3(0, 0, 0);

        controller.enabled = true;
    }

    public void GoToMainMenu()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);

        Time.timeScale = 1f;
        StartCoroutine(sceneTransition.LoadScene(0));
    }
}
