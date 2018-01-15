using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
		}

	}

    public void DropWire(RectTransform endTransform)
    {
		if (state && callPanelState == CallPanelState.Waiting || callPanelState == CallPanelState.Talking)
        {
			if(wire)
			{
				wire.Disconnect ();
			}
            wire = ConnectionLine.Instance.Drop(endTransform, state.person);
			FindObjectsOfType<HabField> ().ToList ().Find (hf => hf.person == ConnectionLine.Instance.startPerson).wire = wire;
			callPanelState = CallPanelState.Talking;
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
	}

	public void Push()
	{
		if(callPanelState == CallPanelState.Waiting || callPanelState == CallPanelState.Incoming)
		{
			DialogController.Instance.Talk (state);
		}

		if(callPanelState == CallPanelState.Talking)
		{
			wire.Listen ();
		}
	}

	void Start()
	{
		DialogController.Instance.onDialogFinished += DialogFinished;
	}

	void DialogFinished(State s1, State s2)
	{
		if((s1==null && s2 == state) || (s1 == state && s2 == null))
		{
			callPanelState = CallPanelState.Waiting;
		}
	}
}
