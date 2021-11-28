using Assets.Scripts.Player;
using UnityEngine;

public class ReverseSectionFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.isReversing = false;
        }
    }
}