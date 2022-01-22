using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator Crossfade;

    public IEnumerator LoadScene(int sceneIndex)
    {
        Crossfade.SetTrigger("Start");
        GameManager.isInterrupted = true;

        yield return new WaitForSeconds(1);

        GameManager.isInterrupted = false;
        SceneManager.LoadScene(sceneIndex);
    }
}
