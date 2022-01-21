using UnityEngine;

public class DashControlsIntro : MonoBehaviour
{
    public GameObject dashControls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            dashControls.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            dashControls.SetActive(false);
        }
    }
}
