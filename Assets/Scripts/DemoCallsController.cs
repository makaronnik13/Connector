using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class DemoCallsController : MonoBehaviour {

	private CallPanel listeningCallPanel = null;
	public List<State> demoStates = new List<State>();

	public Collider2D DropButton, TakeButton;

	private int playedStates = 0;
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
				if(cp.callPanelState == CallPanel.CallPanelState.Off && cp.wire==null)
				{
					return cp;
				}
			}
			return null;
		}
	}

	private CallPanel IncomingPanel
	{
		get
		{
			foreach(CallPanel cp in CallPanels)
			{
				if(cp.callPanelState == CallPanel.CallPanelState.Incoming)
				{
					return cp;
				}
			}
			return null;
		}
	}

	public void StartCalls()
	{
		StartCoroutine(GenerateNewState(BalanceManager.Instance.GetRate(0, playedStates)));
	}

	private IEnumerator GenerateNewState(float remainTime)
	{
		float t = 0;
		while (t<remainTime || EmptyPanel == null)
		{
			if (!Phone.Instance.TalkingPhone) 
			{
				if (EmptyPanel == null && t>=remainTime*2) 
				{
					Phone.Instance.SendWarning (Phone.WarningType.TooSlow);
					t = 0;
				}
				t += 1;
			}
			yield return new WaitForSeconds(1);
		}
			
		if (playedStates < demoStates.Count)
		{
			StorryState s = demoStates[playedStates] as StorryState;
			EmptyPanel.Call(s);
			playedStates++;
			StartCoroutine(GenerateNewState(BalanceManager.Instance.GetRate(0, playedStates)));
		}
	}
		
	public void Listen(CallPanel cp)
	{
		Skip ();
		listeningCallPanel = cp;
		listeningCallPanel.Listen ();
	}

	public void Skip()
	{
		if(listeningCallPanel)
		{
			listeningCallPanel.Skip ();
			listeningCallPanel = null;
		}
	}

	public void Talk()
	{
		listeningCallPanel = null;
	}

	public void AddState(State state)
	{
		Debug.Log ("add state "+state.name);
		demoStates.Insert (playedStates+Random.Range(1,3), state);
	}

	public void Pause()
	{
		if (listeningCallPanel) 
		{
			listeningCallPanel.callPanelState = CallPanel.CallPanelState.Incoming;
			listeningCallPanel = null;
		}
	}

	public void TakeCall()
	{
		CallPanel incomingPanel = IncomingPanel;
		if(incomingPanel)
		{
			incomingPanel.Push ();
		}
	}

	void Update()
	{
		DropButton.enabled = (listeningCallPanel != null);

        Debug.Log(IncomingPanel);
        Debug.Log(listeningCallPanel);

		TakeButton.enabled = (IncomingPanel != null && listeningCallPanel == null);
	}
}
