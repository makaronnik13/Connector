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
    //private float timer = 0;


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
		this._state = state;
		callPanelState = CallPanelState.Incoming;
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
			if (UnityEngine.Random.Range (0, 1f) < _state.DropWarningChance) {
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

    public void DropWire(Transform endTransform)
    {
		if (state && callPanelState == CallPanelState.Waiting || callPanelState == CallPanelState.Talking)
        {
            if (wire)
			{
				wire.Disconnect ();
			}

			Debug.Log ("drop from cp");
            wire = ConnectionLine.Instance.Drop(endTransform, state.person);
			FindObjectsOfType<HabField> ().ToList ().Find (hf => hf.Person == ConnectionLine.Instance.startPerson).wire = wire;
			callPanelState = CallPanelState.Talking;
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

	/*
	public void LaunchTalk(State state)
	{
		this.state = state;
		callPanelState = CallPanelState.Incoming;
	}

    private IEnumerator WaitTimer()
    {
        timer = 0;
        while (timer<state.waitingTime)
        {
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
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

	public void Listen()
	{
		Debug.Log (state);
		DialogController.Instance.OnDialogFinished += MonologFinished;
		DialogController.Instance.PlayMonolog (state);
		GetComponentInChildren<Button> ().interactable = false;
	}

    public void Push()
	{
		if(callPanelState == CallPanelState.Incoming)
		{
			GetComponentInParent<DemoCallsController> ().Listen (this);   
		}

		if(callPanelState == CallPanelState.Waiting)
		{
			Skip ();
		}

		if(callPanelState == CallPanelState.Talking)
		{
			//Skip ();
			//wire.Listen (timer);
		}
	}

	public void Skip()
	{
		if(callPanelState == CallPanelState.Waiting)
		{
			DialogController.Instance.HideDialog ();
			callPanelState = CallPanelState.Off;
			state = null;
			timer = 0;
		}
	}*/
}
