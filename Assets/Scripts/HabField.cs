using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HabField : MonoBehaviour, IWireDraggReciewer
{
    public Person person;
	public Wire wire;
	public bool ServiceHab = false;

    public void StartDragWire(Transform t)
    {
		if(wire)
		{
			wire.Disconnect ();
			wire = null;
		}
        if (person == null)
        {
            return;
        }

        ConnectionLine.Instance.SetStart(t, person);
    }

    public void DropWire(Transform endTransform)
    {
		if (wire != null || person == null)
        {
			return;
        }

		CallPanel callPanel = FindObjectsOfType<CallPanel> ().ToList ().Find (cp => cp.state && cp.state.person == ConnectionLine.Instance.startPerson);  
	
		if (callPanel)
        {
			wire = ConnectionLine.Instance.Drop(endTransform, person);
			callPanel.DropWireToHab ();
			callPanel.wire = wire;
        }
    }

    public void StartDragPaper()
    {
		if (person == null || ServiceHab)
        {
            return;
        }

		GameObject paper = GetComponentInChildren<PaperWithName>().gameObject;
		paper.transform.SetParent(GameObject.FindGameObjectWithTag("GameCanvas").transform);
		paper.transform.localScale = Vector3.one;
		paper.GetComponent<PaperWithName>().Init(person, this);
		person = null;
    }

    public void DropPaper()
    {
		if (person && ServiceHab)
        {
            return;
        }

        PaperWithName paper = FindObjectsOfType<PaperWithName>().ToList().Find(p => p.Dragging);
        if (!paper)
        {
            return;
        }

		PaperWithName currentPaper = GetComponentInChildren<PaperWithName> ();
		if (currentPaper) {
			if (paper.startField) {
				currentPaper.transform.SetParent (paper.startField.GetComponentInChildren<Glass> ().transform.GetChild(0));
				currentPaper.transform.localPosition = Vector3.zero;
               
				paper.startField.person = currentPaper.person;
			} else {
				Destroy (currentPaper.gameObject);
			}
		}

        paper.Dragging = false;
		paper.transform.SetParent(GetComponentInChildren<Glass>().transform.GetChild(0));
        paper.transform.localPosition = Vector3.zero;
        paper.transform.localScale = Vector3.one;
        paper.CancelDestroy();

        person = paper.person;
    }
}
