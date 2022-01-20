using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneTransition sceneTransition;
    private void Start()
    {
        AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.introMusic);
    }

    public void PlayGame()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnStart);

        //Hide cursor and lock it
        Cursor.lockState = CursorLockMode.Locked;

        //Load the next scene in line
        StartCoroutine(sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        AudioManager.GetInstance().StopMusic();
    }

    public void QuitGame()
    {
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);

        Application.Quit();
    }
}
