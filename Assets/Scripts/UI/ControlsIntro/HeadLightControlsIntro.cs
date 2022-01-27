using UnityEngine;

public class HeadLightControlsIntro : MonoBehaviour
{
    public GameObject HeadLightControls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            HeadLightControls.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            HeadLightControls.SetActive(false);
        }
    }
}
