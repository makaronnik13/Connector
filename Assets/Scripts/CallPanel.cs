using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CallPanel : MonoBehaviour
{
	public float waitingTime;

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

	private State _state;
	public State state
	{
		get
		{
			return _state;
		}
		set
		{
			_state = value;
		}
	}

    public SpriteRenderer lamp;
    public Transform hab;

	[HideInInspector]
	public Wire wire;

	void Update()
	{
		if(callPanelState == CallPanelState.Incoming)
		{
			waitingTime += Time.deltaTime;
		}
	}

	public void Push()
	{
		if(Phone.Instance.TalkingPhone)
		{
			return;
		}

		if(callPanelState == CallPanelState.Waiting)
		{
			GetComponentInParent<DemoCallsController> ().Skip(); 
		}

		if(callPanelState == CallPanelState.Incoming)
		{
			GetComponentInParent<DemoCallsController> ().Listen (this);   
		}
	}

	public void Call(State state)
	{
		SoundController.Instance.PlaySound (6);
		this._state = state;
		callPanelState = CallPanelState.Incoming;
		waitingTime = 0;
	}

	public void Listen()
	{
		callPanelState = CallPanelState.Waiting;
		DialogController.Instance.PlayMonolog (_state);
	}

	public void Skip()
	{
		if (_state) 
		{
            float randV = UnityEngine.Random.Range(0, 1f);
            if (randV < _state.DropWarningChance) {
				Phone.Instance.SendWarning (Phone.WarningType.Drop);
			}

			if((_state as StorryState).autoAddState.endPoint!=null)
			{
				FindObjectOfType<DemoCallsController> ().AddState ((_state as StorryState).autoAddState.endPoint);
			}

			if((_state as StorryState).SkipState.endPoint!=null)
			{
				FindObjectOfType<DemoCallsController> ().AddState ((_state as StorryState).SkipState.endPoint);
			}

            if ((_state as StorryState).wrongConnectionNews != null)
            {
                NewsPaper.Instance.SetNews((_state as StorryState).wrongConnectionNews);
            }

            _state = null;
			callPanelState = CallPanelState.Off;
			DialogController.Instance.HideDialog ();
		}
	}

	public void Talk()
	{
		GetComponentInParent<DemoCallsController> ().Talk ();
	}

	public void DropWireToHab()
	{
		if(callPanelState == CallPanelState.Waiting)
		{
			callPanelState = CallPanelState.Talking;
			DialogController.Instance.HideDialog ();
		}
		else if(callPanelState == CallPanelState.Talking)
		{
			callPanelState = CallPanelState.Off;
			state = null;
			wire = null;
        }
	}


    public void StartDragWire(Transform tr)
    {

		if (state && callPanelState == CallPanelState.Waiting)
        {
            ConnectionLine.Instance.SetStart(tr, state.person);
        }

		if(state && callPanelState == CallPanelState.Talking)
		{
			float f = UnityEngine.Random.Range (0, 1f);
			if(f<state.DisconnectWarningChance)
			{
				Phone.Instance.SendWarning (Phone.WarningType.Disconnect);
			}

			state = null;
			callPanelState = CallPanelState.Off;
		}

		if(wire)
		{
			wire.Disconnect ();
			wire = null;
		}
    }
}
