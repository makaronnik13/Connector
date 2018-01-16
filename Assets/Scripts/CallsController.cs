using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CallsController : Singleton<CallsController>
{
    public int day = 1;
	private List<FillerState> fillerStates = new List<FillerState>();
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
		foreach (FillerState state in states)
        {
        	AddState(state);
        }

        StartCoroutine(GenerateNewState());
    }

    private IEnumerator GenerateNewState()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time ++;
            float statesPerMinute = BalanceManager.Instance.balanceAsset.days.ToList().Find(d=>d.day == day).curve.Evaluate(time / 60);
            float statesPerDeltaTime = statesPerMinute / 60;

           

            if (UnityEngine.Random.value<statesPerDeltaTime)
            {

                Debug.Log("!!!");

                if (EmptyPanel)
                {
                    List<FillerState> awaliableStates = fillerStates.Where(s => s.minute <= time).ToList();

                    if (awaliableStates.Count > 0)
                    {
                        FillerState s = awaliableStates[UnityEngine.Random.Range(0, awaliableStates.Count - 1)];
                        EmptyPanel.LaunchTalk(s);
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

	public void RemoveState(FillerState state)
    {
        fillerStates.Remove(state);
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
