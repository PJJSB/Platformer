using Assets.Scripts.Player;
using UnityEngine;

public class ReverseSectionFinish : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            gameManager.isReversing = false;
        }
    }
}