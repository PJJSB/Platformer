using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    enum Difficulty
    {
        Normal, Hard, Vincent
    }
    public static GameManager instance;

    public ReverseSectionEngager reverseSectionEngager;

    public SceneTransition sceneTransition;
    public GameObject pauseMenu;
    public static bool isPaused;
    public static bool isInterrupted;
    public bool isReversing;
    
    public PlayerMovement player;
    
    public Toggle cameraYAxisInversionToggle;
    public CinemachineFreeLook cinemachineFreeLook;
    public GameObject mainCamera;
    private CinemachineBrain cinemachineBrain;
    
    public PlayerStats playerStats;
    [NonSerialized] public TextMeshProUGUI playTime;
    [NonSerialized] public TextMeshProUGUI deaths;

    public Slider difficultySlider;
    
    public Slider postExposureSlider;
    public Volume globalVolume;

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
        pauseMenu.GetComponent<CanvasGroup>().alpha = 0f;
        pauseMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        pauseMenu.GetComponent<CanvasGroup>().interactable = false;

        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;

        //Play explore music
        AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.exploreMusic);

        playTime = pauseMenu.transform.Find("txt_Playtime").GetComponent<TextMeshProUGUI>();
        deaths = pauseMenu.transform.Find("txt_Deaths").GetComponent<TextMeshProUGUI>();

        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        if (player.playerInput.actions["Pause"].triggered && !isInterrupted)
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

    public void Pause()
    {
        cinemachineBrain.enabled = !cinemachineBrain.enabled;

        //Show cursor
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        cinemachineBrain.enabled = !cinemachineBrain.enabled;

        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        Pause();

        pauseMenu.GetComponent<CanvasGroup>().alpha = 1f;
        pauseMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        pauseMenu.GetComponent<CanvasGroup>().interactable = true;

        // Update playtime and deaths in pause menu
        playTime.text = "Playtime: " + playerStats.ReturnTime();
        deaths.text = "Deaths: " + playerStats.deathCount;
    }

    public void ResumeGame()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);
        cinemachineFreeLook.m_YAxis.m_InvertInput = !cameraYAxisInversionToggle.isOn;

        Resume();
        pauseMenu.GetComponent<CanvasGroup>().alpha = 0f;
        pauseMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        pauseMenu.GetComponent<CanvasGroup>().interactable = false;
    }
    public void ChangeDifficulty()
    {
        switch (difficultySlider.value)
        {
            case 0:
                reverseSectionEngager.firstLavaSpeed = 0.3f;
                reverseSectionEngager.secondLavaSpeed = 0.03f;
                break;
            case 1:
                reverseSectionEngager.firstLavaSpeed = 0.4f;        
                reverseSectionEngager.secondLavaSpeed = 0.04f;
                break;
            case 2:
                reverseSectionEngager.firstLavaSpeed = 0.5f;
                reverseSectionEngager.secondLavaSpeed = 0.05f;
                break;
        }
    }
    public void ChangePostExposure()
    {
        var colorAdjustments = globalVolume.profile.TryGet(out ColorAdjustments temp) ? temp : ScriptableObject.CreateInstance<ColorAdjustments>();

        colorAdjustments.postExposure.value = postExposureSlider.value;
    }

    public void GoToMainMenu()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);

        Resume();
        //Show cursor
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.GetComponent<CanvasGroup>().alpha = 0f;
        pauseMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        pauseMenu.GetComponent<CanvasGroup>().interactable = false;

        StartCoroutine(sceneTransition.LoadScene(0));
    }

    public void GoToMainMenuEnd()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);
        isInterrupted = false;
        isPaused = false;

        Time.timeScale = 1f;

        StartCoroutine(sceneTransition.LoadScene(0));
    }
}
