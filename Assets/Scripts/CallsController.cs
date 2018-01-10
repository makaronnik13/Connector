﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CallsController : Singleton<CallsController>
{

	private List<State> fillerStates = new List<State>();
    public float time = 0;

    public float SessionDuration = 60*60;

	public float StateRate = 30;

	private float nextStateTime = 0;

	private List<CallPanel> callPanels;
	private List<CallPanel> CallPanels
	{
		get
		{
			if(callPanels == null)
			{
				callPanels = GetComponentsInChildren<CallPanel> ().ToList();
			}
			return callPanels;
		}
	}

	private CallPanel EmptyPanel
	{
		get{
			foreach(CallPanel cp in CallPanels)
		{
				if(cp.callPanelState == CallPanel.CallPanelState.Off)
				{
					return cp;
				}
		}
			return null;
		}
	}

    void Start()
    {
        time = 0;
        State[] states = Resources.LoadAll<State>("States/Fillers");
        foreach (State state in states)
        {
        	AddState(state);
        }

        DialogController.Instance.onDialogFinished += DialogFinished;
    }

    private void AddState(State state)
    {
        fillerStates.Add(state);
    }

    public void RemoveState(State state)
    {
        fillerStates.Remove(state);
    }

    private void DialogFinished(State arg1, State arg2)
    {
        if (arg1 != null && arg2 != null)
        {
            //add narrative conditions
            foreach (NarrativeLink nl in arg1.narrativeLinks)
            {
                if (nl.endPoint)
                {
                    AddState(nl.endPoint);
                }
            }
            foreach (NarrativeLink nl in arg2.narrativeLinks)
            {
                if (nl.endPoint)
                {
                    AddState(nl.endPoint);
                }
            }
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
		nextStateTime += Time.deltaTime;
		if(nextStateTime >= StateRate)
		{
			Debug.Log ("Time");
			if (EmptyPanel) 
			{
				Debug.Log ("has empty panel");
				Debug.Log (fillerStates.Count);

				List<State> awaliableStates = fillerStates.Where (s=>s.minute<=time).ToList();

				Debug.Log (awaliableStates.Count);
				if (awaliableStates.Count > 0) 
				{
					EmptyPanel.LaunchTalk (awaliableStates [Random.Range (0, awaliableStates.Count - 1)]);
					RemoveState (awaliableStates [Random.Range (0, awaliableStates.Count - 1)]);
				}
			} 
			else 
			{
				Debug.Log ("гамовер");
			}
			nextStateTime = 0;
		}
    }
}
