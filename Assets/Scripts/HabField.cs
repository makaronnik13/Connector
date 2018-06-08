using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HabField : MonoBehaviour, IWireDraggReciewer
{
    public Person InitialPerson;

    private Person person;
    public Person Person
    {
        get
        {
            return person;
        }
        set
        {
            if (wire)
            {
                wire.Disconnect();
                wire = null;
            }
            person = value;
            
        }
    }

	public Wire wire;
	public bool ServiceHab = false;


    void Start()
    {
        Person = InitialPerson;    
    }

    public void StartDragWire(Transform t)
    {
		if(wire)
		{
			wire.Disconnect ();
			wire = null;
		}
        if (Person == null)
        {
            return;
        }

        ConnectionLine.Instance.SetStart(t, Person);
    }

    public void DropWire(Transform endTransform)
    {
		if (Person == null)
        {
			return;
        }

        if (wire != null)
        {
            wire.Disconnect();
            wire = null;
        }

		CallPanel callPanel = FindObjectsOfType<CallPanel> ().ToList ().Find (cp => cp.state && cp.state.person == ConnectionLine.Instance.startPerson);  
	
		if (callPanel)
        {
			wire = ConnectionLine.Instance.Drop(endTransform, Person);
			callPanel.DropWireToHab ();
			callPanel.wire = wire;
        }

        ConnectionLine.Instance.Hide();
    }

    public void StartDragPaper()
    {
		if (Person == null || ServiceHab)
        {
            return;
        }

		GameObject paper = GetComponentInChildren<PaperWithName>().gameObject;
		paper.transform.SetParent(null);
		paper.transform.localScale = Vector3.one*0.01f;
		paper.GetComponent<PaperWithName>().Init(Person, this);
		Person = null;

        
    }

    public void DropPaper()
    {
		if (Person && ServiceHab)
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
               
				paper.startField.Person = currentPaper.person;
			} else {
				Destroy (currentPaper.gameObject);
			}
		}

        paper.Dragging = false;
		paper.transform.SetParent(GetComponentInChildren<Glass>().transform.GetChild(0));
        paper.transform.localPosition = Vector3.zero;
        paper.transform.localScale = Vector3.one;
        paper.CancelDestroy();

        Person = paper.person;
    }
}
