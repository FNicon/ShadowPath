﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public Text actorNameText;
	public Text dialogueText;

	private Animator dialogueAnimator;
	private Queue<string> sentences;
	private float durationPerDialogue;

	// Use this for initialization
	void Start () {
		dialogueAnimator = GetComponent<Animator> ();
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue inputDialogue)
	{
		dialogueAnimator.SetBool("isStartDialogue", true);

		actorNameText.text = inputDialogue.actorName;
		durationPerDialogue = inputDialogue.dialogueDuration;
		sentences.Clear();

		foreach (string sentence in inputDialogue.dialogues)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextDialogue();
	}

	public void DisplayNextDialogue () {
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		StartCoroutine(duration());
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

	IEnumerator duration () {
		yield return new WaitForSeconds (durationPerDialogue);
		DisplayNextDialogue ();
	}

	void EndDialogue() {
		sentences.Clear ();
		dialogueAnimator.SetBool("isStartDialogue", false);
	}
}
