using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CallPanel : MonoBehaviour, IWireDraggReciewer
{

    public enum CallPanelState
    {
        Off,
        Incoming,
        Waiting,
        Talking
    }

	public CallPanelState _callPanelState = CallPanelState.Off;
	public CallPanelState callPanelState
	{
		get
		{
			return _callPanelState;
		}
		set
		{
			_callPanelState = value;
			switch(_callPanelState)
			{
			case CallPanel.CallPanelState.Incoming:
				lamp.color = Color.red;
				break;
			case CallPanel.CallPanelState.Off:
				lamp.color = Color.white;
				break;
			case CallPanel.CallPanelState.Talking:
				lamp.color = Color.green;
				break;
			case CallPanel.CallPanelState.Waiting:
				lamp.color = Color.yellow;
				break;
			}
		}
	}

    public State state;
    public Image lamp;
	public Wire wire;
    private float timer = 0;


	public void DropWire()
	{
		if(callPanelState == CallPanelState.Waiting)
		{
			callPanelState = CallPanelState.Talking;
		}
		else if(callPanelState == CallPanelState.Talking)
		{
			callPanelState = CallPanelState.Off;
			state = null;
			wire = null;
            if (state.canBeDisconnectedAfter<timer)
            {
                Debug.Log("fail (too early disconnect)");
            }
            StopCoroutine(TalkTimer());
            timer = 0;
        }

	}

    public void DropWire(RectTransform endTransform)
    {
		if (state && callPanelState == CallPanelState.Waiting || callPanelState == CallPanelState.Talking)
        {
            DialogController.Instance.OnDialogFinished -= MonologFinished;

            if (wire)
			{
				wire.Disconnect ();
			}


            wire = ConnectionLine.Instance.Drop(endTransform, state.person);
			FindObjectsOfType<HabField> ().ToList ().Find (hf => hf.person == ConnectionLine.Instance.startPerson).wire = wire;
			callPanelState = CallPanelState.Talking;
            StartCoroutine(TalkTimer());
        }
    }

    public void StartDragWire(RectTransform tr)
    {
		if(wire)
		{
			wire.Disconnect ();
			wire = null;
		}

		if (state && callPanelState == CallPanelState.Waiting)
        {
            ConnectionLine.Instance.SetStart(tr, state.person);
        }

		if(state && callPanelState == CallPanelState.Talking)
		{
			state = null;
			callPanelState = CallPanelState.Off;
		}
    }

	public void LaunchTalk(State state)
	{
		this.state = state;
		callPanelState = CallPanelState.Incoming;
        StartCoroutine(WaitTimer());
	}

    private IEnumerator WaitTimer()
    {
        timer = 0;
        while (timer<state.waitingTime)
        {
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        Debug.Log("fail (waiting too long)");
        callPanelState = CallPanelState.Off;
        state = null;
    }

    private IEnumerator TalkTimer()
    {
        timer = 0;

        //replace 0 with path
        float ch = 0;
        foreach (Replica r in state.StateDialog(0).replics)
        {
            ch += r.text.Length;
        }
        

        while (timer<Mathf.Max(state.StateDialog(0).clip.length, ch/BalanceManager.Instance.balanceAsset.charactersPerSecond))
        {
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        Wire wire = FindObjectsOfType<Wire>().FirstOrDefault(w => w.start == state.person && w.end == state.secondPerson());
        if (wire)
        {
            Destroy(wire.gameObject);
        }

        Debug.Log("DialogFinished");
        callPanelState = CallPanelState.Off;
        state = null;
        StopCoroutine(TalkTimer());
        timer = 0;
    }

    public void Push()
	{
		if(callPanelState == CallPanelState.Waiting || callPanelState == CallPanelState.Incoming)
		{
            if (callPanelState == CallPanelState.Waiting)
            {
                StopCoroutine(WaitTimer());
            }
            else
            {
                DialogController.Instance.OnDialogFinished += MonologFinished;
            }
			DialogController.Instance.PlayMonolog (state);
		}

		if(callPanelState == CallPanelState.Talking)
		{
			wire.Listen (timer);
		}
	}

    void MonologFinished(State s1)
    {
        if (s1 == state)
        {
            callPanelState = CallPanelState.Waiting;
        }
    }
}
