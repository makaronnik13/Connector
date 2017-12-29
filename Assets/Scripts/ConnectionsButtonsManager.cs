using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConnectionsButtonsManager : Singleton<ConnectionsButtonsManager> {

    public GameObject pointPrefab;

	// Use this for initialization
	void Start () {
        State[] states = Resources.LoadAll<State>("States");
        Debug.Log(states.Length);

        foreach (State state in states)
        {
            if (state.InNarrativeLinks.Count==0)
            {
                AddState(state);
            }
        }

        DialogController.Instance.onDialogFinished += DialogFinished;

	}

    private void AddState(State state)
    {
        GameObject newPoint = Instantiate(pointPrefab, transform);
        newPoint.transform.localScale = Vector3.one;
        newPoint.GetComponent<ConnectionPoint>().Init(state);
    }

    private void DialogFinished(State arg1, State arg2)
    {
        if (arg1 != null && arg2!=null)
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
}
