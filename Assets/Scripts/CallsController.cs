using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CallsController : Singleton<CallsController>
{

	private List<FillerState> fillerStates = new List<FillerState>();
   
	public AnimationCurve CallsCurve;

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
		FillerState[] states = Resources.LoadAll<FillerState>("States/Fillers");
		foreach (FillerState state in states)
        {
        	AddState(state);
        }

        DialogController.Instance.onDialogFinished += DialogFinished;
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

    private void Update()
    {
		time += Time.deltaTime;

		float statesPerMinute = CallsCurve.Evaluate (time);
		float statesPerDeltaTime = statesPerMinute / 60 * Time.deltaTime;
	
		Debug.Log (statesPerDeltaTime);

		if(nextStateTime >= StateRate)
		{
			if (EmptyPanel) 
			{
				List<FillerState> awaliableStates = fillerStates.Where (s=>s.minute<=time).ToList();

				if (awaliableStates.Count > 0) 
				{
					FillerState s = awaliableStates[Random.Range(0, awaliableStates.Count - 1)];
                    EmptyPanel.LaunchTalk (s);
					RemoveState (s);
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
