using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	public Dialogue dialogue;
	public DialogueManager dialogueScript;
	private bool isAlreadyTrigger;

	void Start() {
		isAlreadyTrigger = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if ((other.gameObject.CompareTag ("Player")) && (isAlreadyTrigger == false)) {
			TriggerDialogue ();
			isAlreadyTrigger = true;
		}
	}

	public void TriggerDialogue ()
	{
		dialogueScript.StartDialogue(dialogue);
	}
}
