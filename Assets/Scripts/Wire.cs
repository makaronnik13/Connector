using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wire : MonoBehaviour {

    private int pathId = 0;
    public Person start;
    public Person end;
	private float dialogTime = 0;

	public void Init(Person start, Person end, int pathId = 0)
    {
        this.pathId = pathId;
        this.start = start;
        this.end = end;
    }

	public void Disconnect()
	{
		//dis
		CallPanel callPanel = FindObjectsOfType<CallPanel> ().ToList ().Find (cp => cp.state && cp.state.person == start); 
		CallPanel callPanel2 = FindObjectsOfType<CallPanel> ().ToList ().Find (cp => cp.state && cp.state.person == end); 
		if(callPanel)
		{
			callPanel.DropWire ();
		}
		if(callPanel2)
		{
			callPanel2.DropWire ();
		}

		Destroy (gameObject);
	}

	void Update()
	{
		dialogTime += Time.deltaTime;
	}

	public void Listen(float time)
	{
        CallPanel callPanel = FindObjectsOfType<CallPanel>().ToList().Find(cp => cp.state && cp.state.person == start);
        CallPanel callPanel2 = FindObjectsOfType<CallPanel>().ToList().Find(cp => cp.state && cp.state.person == end);

        CallPanel call = callPanel;
        if (!call)
        {
            call = callPanel2;
        }

        DialogController.Instance.PlayDialog(call.state, time, pathId);
	}
}
