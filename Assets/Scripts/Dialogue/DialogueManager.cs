﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public Text actorNameText;
	public Text dialogueText;

	private Animator dialogueAnimator;
	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		dialogueAnimator = GetComponent<Animator> ();
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue inputDialogue)
	{
		dialogueAnimator.SetBool("isStartDialogue", true);

		actorNameText.text = inputDialogue.actorName;

		sentences.Clear();

		foreach (string sentence in inputDialogue.dialogues)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextDialogue();
	}

	public void DisplayNextDialogue () {
		if (sentences.Count == 0) {
			EndDialogue ();
			StopAllCoroutines ();
		} else {
			string sentence = sentences.Dequeue ();
			StopAllCoroutines ();
			StartCoroutine (TypeSentence (sentence));
		}
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

	void EndDialogue() {
		dialogueAnimator.SetBool("isStartDialogue", false);
	}
}