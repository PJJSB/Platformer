using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public void PlayGame()
    {
        //Load the next scene in line
        StartCoroutine(sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
