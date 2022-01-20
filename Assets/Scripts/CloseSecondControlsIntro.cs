using UnityEngine;

public class CloseSecondControlsIntro : MonoBehaviour
{
    public GameObject secondControlsIntro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            secondControlsIntro.SetActive(false);
        }
    }
}