using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneTransition SceneTransition;

    public void PlayGame()
    {
        //Load the next scene in line
        StartCoroutine(SceneTransition.LoadScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
