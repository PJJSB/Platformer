using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI finalDeaths;
    public TextMeshProUGUI finalTime;
    public Animator dialogueAnim;
    public Button continueButton;
    public GameManager gameManager;
    public GameObject endTrigger;

    private Queue<string> sentences;
    private GameObject triggerBox;

    // Start is called before the first frame update
    private void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.GetComponent<CanvasGroup>().alpha = 0f;
        dialoguePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        endTrigger.SetActive(false);
    }

    // Gets called when triggered by collision
    public void StartDialogue(Dialogue dialogue, GameObject triggerBox)
    {
        if (gameManager.isReversing)
        {
            endTrigger.SetActive(true);
        }

        this.triggerBox = triggerBox;

        if (triggerBox.name == "EndingTrigger")
        {
            AudioManager.GetInstance().StopMusic();
            AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.introMusic);
        }

        GameManager.isInterrupted = true;

        gameManager.Pause();

        // Panel get activated and first animation gets played
        dialoguePanel.GetComponent<CanvasGroup>().alpha = 1f;
        dialoguePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        dialogueAnim.SetBool("dialogueReset", false);
        dialogueAnim.SetBool("dialogueActive", true);

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
        // Check if there are any sentences
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogue());

            return;
        }

        // Set the text in the UI element
        dialogueText.text = sentences.Dequeue().ToString();
    }

    // Queues the next sentence if possible by pressing a button
    public void DisplayNextSentence()
    {
        // Disables the continue button when transitioning to the next sentence
        continueButton.interactable = false;
        AudioManager.GetInstance().PlaySound(AudioManager.SoundType.uiOnClick);

        if (sentences.Count == 0)
        {
            if (triggerBox.name == "EndingTrigger")
            {
                EndScreenDialogue();
                return;
            }

            StartCoroutine(EndDialogue());
            return;
        }
        //dialogueText.text = sentences.Dequeue().ToString();
        StopAllCoroutines();
        StartCoroutine(PlayNextAnimation());
    }

    // Handles the timing of the transition
    private IEnumerator PlayNextAnimation()
    {
        string sentence = sentences.Dequeue();
        dialogueAnim.SetBool("dialogueNext", true);
        yield return new WaitForSecondsRealtime(1);

        // Set the text in the UI element
        dialogueText.text = sentence;

        yield return new WaitForSecondsRealtime(1);
        dialogueAnim.SetBool("dialogueNext", false);
        continueButton.interactable = true;
    }

    // Gets called when there are no more sentences left
    private IEnumerator EndDialogue()
    {
        dialogueAnim.SetBool("dialogueActive", false);

        yield return new WaitForSecondsRealtime(2);

        dialogueAnim.SetTrigger("dialogueReset");
        GameManager.isInterrupted = false;
        continueButton.interactable = true;
        gameManager.Resume();

        dialoguePanel.GetComponent<CanvasGroup>().alpha = 0f;
        dialoguePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        triggerBox.SetActive(false);
    }

    private void EndScreenDialogue()
    {
        GameManager.isInterrupted = true;

        gameManager.Pause();

        finalDeaths.text = "Deaths: " + gameManager.playerStats.deathCount;
        finalTime.text = "Playtime: " + gameManager.playerStats.ReturnTime();

        dialogueAnim.SetTrigger("dialogueEnd");
    }
}