using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class DialogController: Singleton<DialogController>{

    private State currentState;
    public Action<State> OnDialogFinished = (s) => { };
    public Action OnTypingFinished = () => { };
	private int listsCounter = 0;
	private int currentList = 0;
    public float secondsForSymbol;
	public float dellay = 1;
	private bool scrollInUse = false;
	private bool horizontalInUse = false;

   // public CombinationLink defaultLink;
   
	private Person firstPerson;

	private Stack<Replica> replicasStack = new Stack<Replica>();

	public PersonPanel firstPersonPanel;

    private void Start()
    {
        firstPersonPanel.writer.OnComplete += SectionComplete;
        GetComponentInChildren<Typewriter>().OnComplete += TypingComplete;
    }

    private void TypingComplete()
    {
		listsCounter++;
		currentList++;
        if (currentState)
        {
            currentList = Mathf.Clamp(currentList, 0, currentState.monolog.replics.Count - 1);
        }
        OnTypingFinished.Invoke();
    }

    private void SectionComplete()
    {
        PlayNextReplica();
    }

    public void PlayMonolog(State firstPersonState)
	{
		listsCounter = 0;
		currentList = 0;
        currentState = firstPersonState;
		firstPersonPanel.writer.Reset ();

		firstPerson = firstPersonState.person;
		List<Replica> replics = new List<Replica>(firstPersonState.monolog.replics);

        replics.Reverse();
        replicasStack = new Stack<Replica>(replics);
        replics.Reverse();

		string initial = replics [0].text;

		replics.RemoveAt (0);
		firstPersonPanel.writer.GetComponentInChildren<Typewriter>().Write (initial, replics.Select(r=>r.text).ToArray());

        firstPersonPanel.Show(null, initial);

        PlayNextReplica ();
    }

	public void PlayDialog(State state, float time, int pathId = 0)
	{
		listsCounter = 0;
		currentList = 0;
		firstPersonPanel.writer.Reset ();
        firstPerson = state.person;
        List<Replica> replics = new List<Replica>(state.StateDialog(pathId).replics);

        replics.Reverse();
        replicasStack = new Stack<Replica>(replics);
        replics.Reverse();

        string initial = replics[0].text;
        replics.RemoveAt(0);

		firstPersonPanel.writer.GetComponentInChildren<Typewriter>().Write(initial, replics.Select(r => r.text).ToArray());
        //firstPersonPanel.writer.Write(initial, replics.Select(r => r.text).ToArray());
        PlayNextReplica();
    }

	void Update()
	{
		/*
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			Skip ();   
		}*/

		MoveWheel (Input.GetAxisRaw("Mouse ScrollWheel"));
		MoveKeys (Input.GetAxisRaw("Horizontal"));
	}

	/*
	public void Skip()
	{
        firstPersonPanel.writer.charactersPerSecond = 1000;
    }
	*/	

	public void HideDialog()
    {
            firstPersonPanel.Hide();
            firstPerson = null;
            OnDialogFinished.Invoke(currentState);
            currentState = null;
    }

	private void PlayNextReplica()
	{
        firstPersonPanel.writer.charactersPerSecond = 20;

        if (replicasStack.Count==0 || firstPerson == null)
		{
			firstPerson = null;
			OnDialogFinished.Invoke(currentState);
			//Invoke ("HideDialog", 1);
            return;
		}

        Replica replica = (Replica)replicasStack.Pop ();

		if(replica.person == Dialog.Person.FirstPerson)
		{
			firstPersonPanel.Show (firstPerson.PersonSprite, replica.text);
		}
	}

	private void MoveKeys(float i)
	{
		if(i != 0)
		{
			if(horizontalInUse == false)
			{
				int v = 1;
				if(i<0)
				{
					v = -1;
				}
				MoveReplic (v);

				horizontalInUse = true;
			}
		}
		if(i == 0)
		{
			horizontalInUse = false;
		} 
	}

	private void MoveWheel(float i)
	{
		if(i != 0)
		{
			if(scrollInUse == false)
			{
				int v = 1;
				if(i<0)
				{
					v = -1;
				}
				MoveReplic (v);

				scrollInUse = true;
			}
		}
		if(i == 0)
		{
			scrollInUse = false;
		} 
	}

	private void MoveReplic(int v)
	{
		if(!currentState)
		{
			return;
		}
		if (listsCounter == currentState.monolog.replics.Count) 
		{
			
			if(v>0 && currentList<currentState.monolog.replics.Count-1)
			{
				currentList++;
				GetComponentInChildren<TextMeshProUGUI> ().text = currentState.monolog.replics [currentList].text;
			}

			if(v<0 && currentList>0)
			{
				currentList--;
				GetComponentInChildren<TextMeshProUGUI> ().text = currentState.monolog.replics [currentList].text;
			}
		}
	}
}
