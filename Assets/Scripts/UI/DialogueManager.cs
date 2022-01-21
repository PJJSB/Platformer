using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Animator dialogueAnim;

    private Queue<string> sentences;

    // Start is called before the first frame update
    private void Start()
    {
        sentences = new Queue<string>();
    }

    // Gets called when triggered by collision
    public void StartDialogue(Dialogue dialogue)
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
        
        dialogueAnim.SetBool("dialogueActive", true);
        //Show cursor
        Cursor.lockState = CursorLockMode.None;

        sentences.Clear();

        // Put all sentences in queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayFirstSentence();
    }

    public void DisplayFirstSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Set the text in the UI element
        dialogueText.text = sentences.Dequeue().ToString();
    }

    // Queues the next sentence if possible by pressing a button
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(PlayNextAnimation());


        dialogueAnim.SetTrigger("dialogueStay");
    }

    private IEnumerator PlayNextAnimation()
    {
        dialogueAnim.SetTrigger("dialogueNext");
        yield return new WaitForSeconds(1);

        // Set the text in the UI element
        dialogueText.text = sentences.Dequeue().ToString();
    }

    // Gets called when there are no more sentences left
    private void EndDialogue()
    {
        dialogueAnim.SetBool("dialogueActive", false);
        Destroy(dialoguePanel, 3f);
    }
}