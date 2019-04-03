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

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

}
