using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : Singleton<Phone> 
{
	public enum WarningType
	{
		TooSlow,
		WrongConnection,
		SelfConnection,
		Drop,
		Disconnect,
		ServiceWarning
	}

	public State[] warnings;

	private Queue<State> statesQueue = new Queue<State>();
	public bool TalkingPhone = false;
	private bool talking = false;

	private void Start()
	{
		StartCoroutine(PlayNextCall());
	}

	private IEnumerator PlayNextCall()
	{
		while(true)
		{
			if(!talking && statesQueue.Count>0)
			{
				Ring ();
			}

			yield return new WaitForSeconds (2);
		}
	}

	public void Call(State callState)
	{
		if(!statesQueue.Contains(callState))
		{
			statesQueue.Enqueue (callState);
		}
	}

	private void Ring()
	{
		talking = true;
		GetComponent<Animator> ().SetBool ("Ringing", true);
		GetComponent<AudioSource> ().Play ();
        GetComponent<Collider2D>().enabled = true;
	}

	public void TakePhone()
	{
		FindObjectOfType<DemoCallsController> ().Pause ();
		TalkingPhone = true;
		GetComponent<Animator> ().SetBool ("Ringing", false);
		GetComponent<AudioSource> ().Stop ();
		DialogController.Instance.OnDialogFinished += HangPhone;
		DialogController.Instance.PlayMonolog (statesQueue.Dequeue());
        GetComponent<Collider2D>().enabled = false;
    }

	private void HangPhone()
	{
		HangPhone (null);
	}

	public void HangPhone(State state)
	{
		DialogController.Instance.OnDialogFinished -= HangPhone;
		TalkingPhone = false;
		talking = false;
		DialogController.Instance.HideDialog ();
	}

	public void SendWarning(WarningType warningType, State state = null)
	{
        Debug.Log(warningType);

		switch(warningType)
		{
		case WarningType.SelfConnection:
			Call (warnings[(int)warningType]);
			break;
		case WarningType.ServiceWarning:
			Call (state);
			break;
		case WarningType.TooSlow:
			Call (warnings[(int)warningType]);
			break;
		case WarningType.Drop:
			Call (warnings[(int)warningType]);
			break;
		case WarningType.WrongConnection:
			Call (warnings[(int)warningType]);
			break;
		case WarningType.Disconnect:
			Call (warnings[(int)warningType]);
			break;
		}
	}
}
