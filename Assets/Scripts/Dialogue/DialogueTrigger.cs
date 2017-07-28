using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	public Dialogue dialogue;
	public DialogueManager dialogueScript;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			TriggerDialogue ();
		}
	}

	public void TriggerDialogue ()
	{
		dialogueScript.StartDialogue(dialogue);
	}
}
