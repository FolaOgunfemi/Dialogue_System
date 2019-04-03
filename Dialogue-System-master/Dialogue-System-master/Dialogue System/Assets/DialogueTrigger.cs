using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initiates a new Dialogue 
/// </summary>
public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

    /// <summary>
    /// Initiates a new Dialogue 
    /// </summary>
    public void TriggerDialogue ()
	{
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        DialogueManager.Instance.StartDialogue(dialogue);

    }

}
