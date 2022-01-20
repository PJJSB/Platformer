using UnityEngine;

public class OpenSecondControlsIntro : MonoBehaviour
{
    public GameObject secondControlsIntro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            secondControlsIntro.SetActive(true);
        }
    }
}