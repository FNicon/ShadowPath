using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	public Dialogue dialogue;
	public DialogueManager dialogueScript;


	public void TriggerDialogue ()
	{
		dialogueScript.StartDialogue(dialogue);
	}
}
