using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject dialogueUI;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Button previousButton;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        EnablePreviousButton();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueUI.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

        foreach(string sentence in dialogue.sentences)
        {
            
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
        }
        string sentence = sentences.Dequeue();

        //This is if you want text to appear instantly.
        //dialogueText.text = sentnece;


        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayPreviousSentence()
    {
        
    }
    /// <summary>
    /// Will pop up each character for the text. Each character will go up per second.
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EnablePreviousButton()
    {
        //if (backSentences.Count == 0)
        //{
        //    previousButton.gameObject.SetActive(false);
        //}
        //else
        //{
        //    previousButton.gameObject.SetActive(true);
        //}
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        return;
    }
}
