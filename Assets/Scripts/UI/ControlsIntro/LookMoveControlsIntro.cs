using System;
using UnityEngine;

public class LookMoveControlsIntro : MonoBehaviour
{
    public GameObject lookControls;
    public GameObject moveControls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            lookControls.SetActive(true);
            moveControls.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Character")
        {
            lookControls.SetActive(false);
            moveControls.SetActive(false);
        }
    }
}