using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wire : MonoBehaviour {

    public Person start;
    public Person end;
	private float dialogTime = 0;

	public void Init(Person start, Person end)
    {
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

	public void Listen()
	{
		//DialogController.Instance.Talk (start, end);
	}
}
