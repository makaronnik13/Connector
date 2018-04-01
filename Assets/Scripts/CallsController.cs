using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CallsController : Singleton<CallsController>
{
    public int day = 1;
	private List<FillerState> fillerStates = new List<FillerState>();
    private List<StorryState> activeStorryStates = new List<StorryState>();
    private List<StorryState> allStorryStates = new List<StorryState>();
	private float time = 0;
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
		FillerState[] states = Resources.LoadAll<FillerState>("States/Fillers");
        allStorryStates = Resources.LoadAll<StorryState>("States/StorryStates").ToList();
        foreach (FillerState state in states)
        {
        	AddState(state);
        }

        foreach (StorryState state in allStorryStates)
        {
            AddStorryState(state);
        }

        StartCoroutine(GenerateNewFillerState());
        StartCoroutine(GenerateNewStorryState());
    }

    private IEnumerator GenerateNewStorryState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            
                if (EmptyPanel)
                {
                    List<StorryState> awaliableStates = activeStorryStates.Where(s => s.minute <= time && day == s.day).ToList();

                    if (awaliableStates.Count > 0)
                    {
                        StorryState s = awaliableStates[UnityEngine.Random.Range(0, awaliableStates.Count - 1)];
					EmptyPanel.Call(s);
                        RemoveState(s);
                    }
                }
                else
                {
                    Debug.Log("no free call hubs");
                }
        }
    }

    private IEnumerator GenerateNewFillerState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time ++;
            float statesPerMinute = BalanceManager.Instance.balanceAsset.days.ToList().Find(d=>d.day == day).fillersRate.Evaluate(time / 60);
            float statesPerDeltaTime = statesPerMinute / 60;

           

            if (UnityEngine.Random.value<statesPerDeltaTime)
            {

				Debug.Log(gameObject);

                if (EmptyPanel)
                {
                    List<FillerState> awaliableStates = fillerStates.Where(s => s.minute <= time && day == s.day).ToList();

                    if (awaliableStates.Count > 0)
                    {
                        FillerState s = awaliableStates[UnityEngine.Random.Range(0, awaliableStates.Count - 1)];
						EmptyPanel.Call(s);
                        RemoveState(s);
                    }
                }
                else
                {
                    Debug.Log("no free call hubs");
                }
            }
        }
    }

    private void AddState(FillerState state)
    {
        fillerStates.Add(state);
    }

    private void AddStorryState(StorryState state)
    {
        if (InLinks(state).Count == 0)
        {
            activeStorryStates.Add(state);
        }
    }

    private List<StorryState> InLinks(StorryState state)
    {
        List<StorryState> inStates = new List<StorryState>();

        foreach (StorryState ss in allStorryStates)
        {
            if (ss.wrongConnectionState.endPoint == ss)
            {
                inStates.Add(ss);
            }

            foreach (Link l in ss.combinationLinks)
            {
                if (l.endPoint == state)
                {
                    inStates.Add(ss);
                }
            }
        }

        return inStates;
    }

    public void RemoveState(FillerState state)
    {
        fillerStates.Remove(state);
    }

    public void RemoveState(StorryState state)
    {
        activeStorryStates.Remove(state);
    }

    private void DialogFinished(State arg1, State arg2)
    {
        /*
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
        }*/
    }

}
