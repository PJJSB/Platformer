using Assets.Scripts.Player;
using UnityEngine;

public class ReverseSectionEngager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            Player player = other.GetComponent<Player>();
            player.isReversing = true;
        }
    }
}