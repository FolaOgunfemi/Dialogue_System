using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;
    [Tooltip ("As the user requests new senetneces we will load them from the end of this queu")]
	private Queue<string> sentences;


    #region Singleton_Logic
    ///start//// SINGLETON LOGIC

    private DialogueManager()
    {

    }

    private static DialogueManager instance = null;

    public static DialogueManager GetInstance()
    {
        // create the instance only if the instance is null
        if (instance == null)
        {
            instance = new DialogueManager();
        }
        // Otherwise return the already existing instance
        return instance;
    }

    // Game Instance Singleton
    public static DialogueManager Instance
    {
        get
        {
            return instance;
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    ///end//// SINGLETON LOGIC
    #endregion

    // Use this for initialization
    void Start () {
		sentences = new Queue<string>();
	}

    /// <summary>
    /// Begins a new discussion, clearing the old one
    /// </summary>
    /// <param name="dialogue"></param>
	public void StartDialogue (Dialogue dialogue)
	{
        //Tell the animator to open the discussion window
		animator.SetBool("IsOpen", true);
        //Set the speaker's name
		nameText.text = dialogue.name;
        //Clear the old sentences before populating
		sentences.Clear();
        //Queue up all of the sentences
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

    /// <summary>
    /// Display conversation text, spelling out one letter at a time
    /// </summary>
	public void DisplayNextSentence ()
	{
        //End the discussion if we run out of sentences
		if (sentences.Count == 0)
		{
            //end the discussion
			EndDialogue();
			return;
		}

        //1. unload one sentence from the queue
		string sentence = sentences.Dequeue();
        //2. Stop Coroutines so that we can start the next one fresh
		StopAllCoroutines();
        //3. type the current sentence one character at a time
		StartCoroutine(TypeSentence(sentence));
	}

    /// <summary>
    /// Call this coroutine to type the current sentence one character at a time
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

    /// <summary>
    /// Tells the Animator that the window should be closed, ending the discussion
    /// </summary>
	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

}
