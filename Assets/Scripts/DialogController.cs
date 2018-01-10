using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class DialogController: Singleton<DialogController>{

	public float secondsForSymbol;
	public float dellay = 1;

    public CombinationLink defaultLink;

	private State firstState, secondState;

	private Stack<Replica> replicasStack = new Stack<Replica>();

    private bool canFinish = false;

	public PersonPanel firstPersonPanel, secondPersonPanel;

	public Action<State,State> onDialogFinished = (State s1, State s2)=>{};

	public void Talk(State firstPersonState)
	{
		firstState = firstPersonState;
		List<Replica> replics = new List<Replica>(firstState.monolog.replics);
		replics.Reverse ();
		replicasStack = new Stack<Replica>(replics);
		PlayNextReplica ();
	}

	public void Talk(State firstPersonState, State secondPersonState)
	{
        CombinationLink link = null;

        foreach (CombinationLink cl in firstPersonState.combinationLinks)
        {
            Debug.Log(cl.endPoint);

            if (cl.endPoint == secondPersonState)
            {
                link = cl;
            }
        }

        foreach (CombinationLink cl in secondPersonState.combinationLinks)
        {

            if (cl.endPoint == firstPersonState)
            {
                link = cl;

                State tempState = firstPersonState;
                firstPersonState = secondPersonState;
                secondPersonState = tempState;

            }
        }

        Debug.Log(link);

        if (link == null)
        {
            link = defaultLink;
        }

		firstState = firstPersonState;
		secondState = secondPersonState;
		List<Replica> replics = new List<Replica>(link.dialog.replics);
		replics.Reverse ();
		replicasStack = new Stack<Replica>(replics);
		PlayNextReplica ();
	}

	void Update()
	{
		/*
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Skip ();

            if (canFinish)
            {
                HideDialog();
            }

		}*/
	}

	public void Skip()
	{
		CancelInvoke ("PlayNextReplica");
		PlayNextReplica ();
	}

	private void HideDialog()
	{
		Debug.Log ("HideDialog");

            firstPersonPanel.Hide();
            secondPersonPanel.Hide();
            Rotator.Instance.rotating = true;
            onDialogFinished.Invoke(firstState, secondState);
            firstState = null;
            secondState = null;
        canFinish = false;
	}

	private void PlayNextReplica()
	{
        canFinish = false;

        if (replicasStack.Count==0)
		{
            canFinish = true;
			Invoke ("HideDialog", 2);
            return;
		}

        Rotator.Instance.rotating = false;

        Replica replica = (Replica)replicasStack.Pop ();

		if(replica.person == Dialog.Person.FirstPerson)
		{

			firstPersonPanel.Show (firstState.person.PersonSprite, replica.text);
		}
		if(replica.person == Dialog.Person.SecondPerson)
		{
			secondPersonPanel.Show (secondState.person.PersonSprite, replica.text);
		}

		Invoke ("PlayNextReplica", replica.text.Length*secondsForSymbol+dellay);
	}

}
