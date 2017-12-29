using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class DialogController: Singleton<DialogController>{

	public float secondsForSymbol;
	public float dellay = 1;

	private bool dialogFinished = false;

	private State firstState, secondState;

	private Stack<Replica> replicasStack;

	public PersonPanel firstPersonPanel, secondPersonPanel;

	public Action<State,State> onDialogFinished = (State s1, State s2)=>{};

	public void Talk(State firstPersonState)
	{
		firstPersonPanel.GetComponent<Animator> ().SetBool ("Single", true);
		dialogFinished = false;
		firstState = firstPersonState;
		List<Replica> replics = new List<Replica>(firstState.monolog.replics);
		replics.Reverse ();
		replicasStack = new Stack<Replica>(replics);
		PlayNextReplica ();
	}

	public void Talk(State firstPersonState, NarrativeLink link)
	{
		firstPersonPanel.GetComponent<Animator> ().SetBool ("Single", false);
		dialogFinished = false;
		firstState = firstPersonState;
		secondState = link.endPoint;
		List<Replica> replics = new List<Replica>(link.dialog.replics);
		replics.Reverse ();
		replicasStack = new Stack<Replica>(replics);
		PlayNextReplica ();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Skip ();
		}
	}

	public void Skip()
	{
		if (dialogFinished) {
			HideDialog ();
		} else 
		{
			CancelInvoke ("PlayNextReplica");
			PlayNextReplica ();
		}
	}

	private void HideDialog()
	{
		firstPersonPanel.Hide ();
		secondPersonPanel.Hide ();
		Rotator.Instance.rotating = true;
		onDialogFinished.Invoke (firstState, secondState);
		firstState = null;
		secondState = null;
	}

	private void PlayNextReplica()
	{
		if(replicasStack.Count==0)
		{
			dialogFinished = true;
			return;
		}
		Replica replica = (Replica)replicasStack.Pop ();

		if(replica.person == Dialog.Person.FirstPerson)
		{
			firstPersonPanel.Show (firstState.Sprite, replica.text);
		}
		if(replica.person == Dialog.Person.SecondPerson)
		{
			secondPersonPanel.Show (secondState.Sprite, replica.text);
		}

		Invoke ("PlayNextReplica", replica.text.Length*secondsForSymbol+dellay);
	}
}
