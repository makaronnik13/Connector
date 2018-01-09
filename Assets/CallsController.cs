using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallsController : Singleton<CallsController>
{

    private List<State> currentStates = new List<State>();
    public float time = 0;

    public float SessionDuration = 60*60;

    void Start()
    {
        time = 0;
        State[] states = Resources.LoadAll<State>("States");
        foreach (State state in states)
        {
            if (state.InNarrativeLinks.Count == 0)
            {
                AddState(state);
            }
        }
        DialogController.Instance.onDialogFinished += DialogFinished;
    }

    private void AddState(State state)
    {
        currentStates.Add(state);
    }

    public void RemoveState(State state)
    {
        currentStates.Remove(state);
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

            RemoveState(arg1);
            RemoveState(arg2);
        }
    }

    private void Update()
    {
        time += Time.deltaTime * SessionDuration / 24 / 60 / 60;

    }
}
