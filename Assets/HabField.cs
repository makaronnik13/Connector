using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HabField : MonoBehaviour, IWireDraggReciewer
{
    public Person person;

    public void StartDragWire(RectTransform t)
    {
        if (person == null)
        {
            return;
        }

        ConnectionLine.Instance.SetStart(t, person);
    }

    public void DropWire(RectTransform endTransform)
    {
        if (person == null || ConnectionLine.Instance.startPerson == person)
        {
            return;
        }

        if (FindObjectsOfType<CallPanel>().ToList().Find(cp=>cp.state && cp.state.person == ConnectionLine.Instance.startPerson))
        {
            ConnectionLine.Instance.Drop(endTransform, person);
        }
    }

    public void StartDragPaper()
    {
        if (!person.Service || person == null)
        {
            return;
        }


    }

    public void DropPaper()
    {
        if (person && person.Service)
        {
            return;
        }

        PaperWithName paper = FindObjectsOfType<PaperWithName>().ToList().Find(p => p.Dragging);
        if (!paper)
        {
            return;
        }
        paper.Dragging = false;
        paper.transform.SetParent(GetComponentInChildren<Glass>().transform);
        paper.transform.localPosition = Vector3.zero;
        paper.CancelDestroy();

        person = paper.person;
    }
}
