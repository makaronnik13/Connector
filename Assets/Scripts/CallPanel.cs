using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
				lamp.color = Color.grey;
				break;
			case CallPanel.CallPanelState.Waiting:
				lamp.color = Color.yellow;
				break;
			}
		}
	}

    public State state;
    public Image lamp;

    public void DropWire(RectTransform endTransform)
    {
        if (state)
        {
            ConnectionLine.Instance.Drop(endTransform, state.person);
        }
    }

    public void StartDragWire(RectTransform tr)
    {
        if (state)
        {
            ConnectionLine.Instance.SetStart(tr, state.person);
        }
    }

	public void LaunchTalk(State state)
	{
		this.state = state;
		callPanelState = CallPanelState.Incoming;
	}

	public void Push()
	{
		Debug.Log (callPanelState);

		if(callPanelState == CallPanelState.Waiting || callPanelState == CallPanelState.Incoming)
		{
			Debug.Log ("Talk");
			DialogController.Instance.Talk (state);
		}

		if(callPanelState == CallPanelState.Talking)
		{
			//listen dialog
		}
	}

	void Start()
	{
		DialogController.Instance.onDialogFinished += DialogFinished;
	}

	void DialogFinished(State s1, State s2)
	{
		Debug.Log ("Dialog finished");
		if((s1==null && s2 == state) || (s1 == state || s2 == null))
		{
			callPanelState = CallPanelState.Waiting;
		}
	}
}
