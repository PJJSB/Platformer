using UnityEngine;

public class DoubleJumpControlsIntro : MonoBehaviour
{
    public GameObject doubleJumpControls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            doubleJumpControls.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            doubleJumpControls.SetActive(false);
        }
    }
}
