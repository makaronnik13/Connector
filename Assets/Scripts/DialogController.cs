using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class DialogController: Singleton<DialogController>{

    private State currentState;
    public Action<State> OnDialogFinished = (s) => { };


    public float secondsForSymbol;
	public float dellay = 1;

    public CombinationLink defaultLink;
   
	private Person firstPerson, secondPerson;

	private Stack<Replica> replicasStack = new Stack<Replica>();

	public PersonPanel firstPersonPanel, secondPersonPanel;

    private void Start()
    {
        firstPersonPanel.writer.OnComplete += SectionComplete;
    }

    private void SectionComplete()
    {
        PlayNextReplica();
    }

    public void PlayMonolog(State firstPersonState)
	{
        currentState = firstPersonState;
		firstPersonPanel.writer.Reset ();

		firstPerson = firstPersonState.person;
		List<Replica> replics = new List<Replica>(firstPersonState.monolog.replics);

        replics.Reverse();
        replicasStack = new Stack<Replica>(replics);
        replics.Reverse();

		string initial = replics [0].text;
		replics.RemoveAt (0);
		firstPersonPanel.writer.Write (initial, replics.Select(r=>r.text).ToArray());

        PlayNextReplica ();
    }

	public void PlayDialog(State state, float time, int pathId = 0)
	{
		firstPersonPanel.writer.Reset ();
        firstPerson = state.person;
        secondPerson = state.secondPerson();
        List<Replica> replics = new List<Replica>(state.StateDialog(pathId).replics);

        replics.Reverse();
        replicasStack = new Stack<Replica>(replics);
        replics.Reverse();

        string initial = replics[0].text;
        replics.RemoveAt(0);
        firstPersonPanel.writer.Write(initial, replics.Select(r => r.text).ToArray());
        PlayNextReplica();
    }

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			Skip ();   
		}
	}

	public void Skip()
	{
        firstPersonPanel.writer.charactersPerSecond = 1000;
    }

    private void HideDialog()
    {
        if (firstPerson != null || secondPerson != null)
        {
            firstPersonPanel.Hide();
            secondPersonPanel.Hide();
            firstPerson = null;
            secondPerson = null;
            OnDialogFinished.Invoke(currentState);
            currentState = null;
        }
    }

	private void PlayNextReplica()
	{
        firstPersonPanel.writer.charactersPerSecond = 10;

        if (replicasStack.Count==0 || firstPerson == null)
		{
			Invoke ("HideDialog", 1);
            return;
		}

        Replica replica = (Replica)replicasStack.Pop ();

		if(replica.person == Dialog.Person.FirstPerson)
		{
			firstPersonPanel.Show (firstPerson.PersonSprite, replica.text);
		}
		if(replica.person == Dialog.Person.SecondPerson)
		{
			secondPersonPanel.Show (secondPerson.PersonSprite, replica.text);
		}
	}

}
