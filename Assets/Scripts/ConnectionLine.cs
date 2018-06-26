using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectionLine : Singleton<ConnectionLine> {

    public float coef = 1;

    public Color[] colors;

    public Person startPerson;

    public GameObject Jack;

    private Material material;
    private List<Vector3> points = new List<Vector3>();

	public Wire Drop(Transform endTransform, Person endPerson)
    {
        SoundController.Instance.PlaySound(0);

        //fake wrong number
        CallPanel callPanel = FindObjectsOfType<CallPanel>().First(cp => cp.state && (cp.state.person == startPerson || cp.state.person == endPerson));
        if (callPanel)
        {
            Person p = startPerson;
            if (callPanel.state.person == startPerson)
            {
                p = endPerson;
            }
            
			if (!callPanel.state.secondPersons ().Contains(p)) {
				//Disconnect
				Phone.WarningType warningType = Phone.WarningType.WrongConnection;

				if(endPerson == startPerson)
				{
					warningType = Phone.WarningType.SelfConnection;
				}
				else
				{
					if(endPerson.wrongConnectionState)
					{
						warningType = Phone.WarningType.ServiceWarning;
					}
				}           

                StartCoroutine (CauseConnectionFail (callPanel, warningType, endPerson.wrongConnectionState));
			} else 
			{
                
                StartCoroutine (CauseConnectionEnd (callPanel, BalanceManager.Instance.balanceAsset.GetTalkingTime(callPanel.state.TalkingTime)));
			}

			callPanel.Talk ();
        }

        GameObject newWire = new GameObject();

        //send pathId for story state
        newWire.AddComponent<Wire>().Init(startPerson, endPerson);

        newWire.transform.SetParent(transform);
        LineRenderer lr = newWire.AddComponent<LineRenderer>();
        lr.material = new Material(material);
        lr.useWorldSpace = true;
        lr.SetWidth(GetComponent<LineRenderer>().startWidth, GetComponent<LineRenderer>().endWidth);

        Vector3 start = new Vector3(startTransform.position.x, startTransform.position.y, -0.3f);
        Vector3 end = new Vector3(endTransform.position.x, endTransform.position.y, -0.3f);
        points.Clear();
        for (int i = 0; i < 10; i++)
        {
            points.Add(new Vector3(Mathf.Lerp(start.x, end.x, (i + .0f) / 10), Mathf.Lerp(start.y, end.y, (float)Math.Pow((double)((i + .0f) / 10), (double)2f)), start.z));
        }
        points.Add(end);
        points = LineSmoother.SmoothLine(points.ToArray(), 0.3f).ToList();



        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
        lr.numCapVertices = 8;
		startPerson = null;
		return newWire.GetComponent<Wire> ();
    }

	private IEnumerator CauseConnectionFail(CallPanel callPanel, Phone.WarningType warningType, State wrongConnectionState = null)
	{
		float t = 0;
		while(t< BalanceManager.Instance.balanceAsset.GetTalkingTime(BalanceManager.Instance.balanceAsset.WrongTalkingTime))
		{
			t += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
			
		if(warningType == Phone.WarningType.ServiceWarning)
		{
			Phone.Instance.SendWarning (Phone.WarningType.ServiceWarning, wrongConnectionState);
		}
		if(warningType == Phone.WarningType.WrongConnection)
		{
			float f = UnityEngine.Random.Range (0, 1f);
			if (callPanel.state) 
			{
				if (f < callPanel.state.WrongConnectionWarningChance) {
					Phone.Instance.SendWarning (Phone.WarningType.WrongConnection);
				}
			}
		}
		if(warningType == Phone.WarningType.SelfConnection)
		{
			Phone.Instance.SendWarning (Phone.WarningType.SelfConnection);
		}


        if (callPanel.state)
        {

            if (callPanel.state.GetType() == typeof(StorryState))
            {
                if ((callPanel.state as StorryState).wrongConnectionState.endPoint)
                {
                    FindObjectOfType<DemoCallsController>().AddState((callPanel.state as StorryState).wrongConnectionState.endPoint);
                }
            }
            if (callPanel.state.GetType() == typeof(StorryState))
            {
                if ((callPanel.state as StorryState).autoAddState.endPoint)
                {
                    FindObjectOfType<DemoCallsController>().AddState((callPanel.state as StorryState).autoAddState.endPoint);
                }
            }
        }

            callPanel.state = null;
		callPanel.callPanelState = CallPanel.CallPanelState.Off;
        if (callPanel.wire)
        {
            callPanel.wire.Disconnect();
        }
		
	}

    private IEnumerator CauseConnectionEnd(CallPanel callPanel, float time)
    {
        float t = 0;
        while (t < time)
        {
            if (!Phone.Instance.TalkingPhone)
            {
                t += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }


        if(callPanel.state)
        { 
        if (callPanel.state.GetType() == typeof(StorryState))
        {
            if ((callPanel.state as StorryState).autoAddState.endPoint)
            {
                FindObjectOfType<DemoCallsController>().AddState((callPanel.state as StorryState).autoAddState.endPoint);
            }
        }

        if (callPanel.state.GetType() == typeof(StorryState))
        {
            foreach (CombinationLink cl in (callPanel.state as StorryState).combinationLinks)
            {
                    if (callPanel.wire)
                    {
                        if (cl.person == callPanel.wire.end && cl.endPoint)
                        {
                            FindObjectOfType<DemoCallsController>().AddState(cl.endPoint);
                        }
                    }
            }
        }
    }

        callPanel.state = null;
		callPanel.callPanelState = CallPanel.CallPanelState.Off;
		if(callPanel.wire)
		{
			callPanel.wire.Disconnect ();
		}
	}


    private Transform startTransform;
    private LineRenderer line;
    private LineRenderer Line
    {
        get
        {
            if (!line)
            {
                line = GetComponent<LineRenderer>();
            }
            return line;
        }
    }


    #region Lifecycle
    private void Update()
    {
        Jack.SetActive(Line.enabled);
        if (Line.enabled)
        {
            Vector3 start = new Vector3(startTransform.position.x, startTransform.position.y, -5);
            Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));

            points.Clear();

			int blocks = Mathf.Clamp(Mathf.RoundToInt(Vector3.Distance (start, end)), 2, 100);
			for (int i = 0; i<blocks;i++)
            {
				points.Add(new Vector3(Mathf.Lerp(start.x, end.x, (i+.0f)/blocks), Mathf.Lerp(start.y, end.y, (float)Math.Pow((double)((i + .0f) / blocks),(double)2f)), start.z));
            }

            points.Add(end);

            points = LineSmoother.SmoothLine(points.ToArray(), 0.3f).ToList();

			points.RemoveRange (points.Count-1, 1);

            Line.positionCount = points.Count;

            Line.SetPositions(points.ToArray());

            Jack.transform.position = points[points.Count-1];

            float div = (points[points.Count - 1].x - points[points.Count - 2].x) / (points[points.Count - 1].y - points[points.Count - 2].y);
            float angle = Mathf.Atan(div) * Mathf.Rad2Deg;
            if (points[points.Count - 1].y < points[points.Count - 2].y)
            {
                angle = angle-180;
            }
            Jack.transform.localRotation = Quaternion.Euler(0,0,360-angle);
        }
        
        
    }

    private void Start()
    {
        material = GetComponent<LineRenderer>().material;
        Hide();
    }
    #endregion

    public void Hide()
    {
        startTransform = null;
        Line.enabled = false;
    }

    public void SetStart(Transform start, Person person)
    {

        if (start == startTransform)
        {
            return;
        }
			
        startPerson = person;

        Color c = colors[UnityEngine.Random.Range(0, colors.Count() - 1)];
        material.color = c;

        if (startTransform!=start)
        {
            startTransform = start;
            Line.enabled = true;
        }
        
    }

	

   
}
