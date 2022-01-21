using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}