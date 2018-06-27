using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class DemoCallsController : Singleton<DemoCallsController> {

	public CallPanel listeningCallPanel = null;
	public List<State> demoStates = new List<State>();

	public bool Calling;

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

	private bool HaveCalls
	{
		get
		{
			foreach(CallPanel cp in CallPanels)
			{
				if(cp.callPanelState == CallPanel.CallPanelState.Incoming || cp.callPanelState == CallPanel.CallPanelState.Waiting)
				{
					return true;
				}
			}
			return false;
		}
	}

	private CallPanel IncomingPanel
	{
		get
		{
			List<CallPanel> panels = CallPanels.OrderByDescending (c=>c.waitingTime).Where(cp=>cp.callPanelState == CallPanel.CallPanelState.Incoming).ToList();
			if(panels.Count>0)
			{
				return panels.First();
			}
			return null;
		}
	}

	public void StartCalls()
	{
		Calling = true;
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
			Calling = false;
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
        if (listeningCallPanel)
		{
            if (((StorryState)listeningCallPanel.state).EndingCall)
            {
                Debug.Log("the end");
            }

            listeningCallPanel.Skip ();
			listeningCallPanel = null;
            ConnectionLine.Instance.Hide();
            SoundController.Instance.PlaySound(2);
		}
	}

	public void Talk()
	{
		listeningCallPanel = null;
	}

	public void AddState(State state)
	{
		int position = playedStates + Random.Range (1, 3);
		position = Mathf.Clamp (position, 0, demoStates.Count-1);
		demoStates.Insert (position, state);
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
        if (incomingPanel)
		{
            incomingPanel.Push ();
            SoundController.Instance.PlaySound(1);
            ConnectionLine.Instance.SetStart(incomingPanel.hab, incomingPanel.state.person);
        }

        
    }

	void Update()
	{
		DropButton.enabled = (listeningCallPanel != null || Tutorial.Instance.DropEnabled);


		TakeButton.enabled = (IncomingPanel != null && listeningCallPanel == null && !Phone.Instance.TalkingPhone);

        if (EmptyPanel)
        {
			if (HaveCalls) {
				WarningLamp.Instance.SetState(WarningLamp.WarningLampState.yellowLamp);
			} else 
			{
				WarningLamp.Instance.SetState(WarningLamp.WarningLampState.defaultLamp);	
			}
            
        }
        else
        {
            WarningLamp.Instance.SetState(WarningLamp.WarningLampState.warningLamp);
        }
    }
}
