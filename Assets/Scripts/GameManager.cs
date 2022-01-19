using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ReverseSectionEngager reverseSectionEngager;

    public SceneTransition sceneTransition;
    public GameObject pauseMenu;
    public static bool isPaused;
    public bool isReversing;
    
    public GameObject hubRespawnAnchor;
    public PlayerMovement player;
    
    public Toggle cameraYAxisInversionToggle;
    public CinemachineFreeLook cinemachineFreeLook;
    public GameObject mainCamera;
    private CinemachineBrain cinemachineBrain;
    
    public PlayerStats playerStats;
    private TextMeshProUGUI _playTime;
    private TextMeshProUGUI _deaths;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        //Set pause menu to deactive as default
        pauseMenu.SetActive(false);

        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;

        //Play explore music
        AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.exploreMusic);

        _playTime = pauseMenu.transform.Find("txt_Playtime").GetComponent<TextMeshProUGUI>();
        _deaths = pauseMenu.transform.Find("txt_Deaths").GetComponent<TextMeshProUGUI>();

        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
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

    public void ResetReversal()
    {
        reverseSectionEngager.ResetReversal();
    }

    public void PauseGame()
    {
        cinemachineBrain.enabled = !cinemachineBrain.enabled;

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
        cinemachineBrain.enabled = !cinemachineBrain.enabled;

        cinemachineFreeLook.m_YAxis.m_InvertInput = !cameraYAxisInversionToggle.isOn;
        
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
        cinemachineBrain.enabled = !cinemachineBrain.enabled;
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);

        pauseMenu?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        StartCoroutine(sceneTransition.LoadScene(0));
    }
}
