using UnityEngine;

public class JumpControlsIntro : MonoBehaviour
{
    public GameObject jumpControls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            jumpControls.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            jumpControls.SetActive(false);
        }
    }
}