using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public GameObject pauseMenu;
    public bool isPaused;
    
    public GameObject hubRespawnAnchor;
    public PlayerMovement player;

    private void Start()
    {
        //Set pause menu to deactive as default
        pauseMenu.SetActive(false);

        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;
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
