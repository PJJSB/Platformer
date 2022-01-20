using System;
using UnityEngine;

public class CloseFirstControlsIntro : MonoBehaviour
{
    public GameObject firstControlsIntro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            firstControlsIntro.SetActive(false);
        }
    }
}