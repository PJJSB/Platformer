using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSectionEngager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            var player = other.GetComponent<PlayerStateManager>();
            player.isReverseSection = true;
        }
    }
}
