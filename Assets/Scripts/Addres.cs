using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Addres : MonoBehaviour, IDragHandler {

    private Person person;

	public void Init(Person p)
    {
        person = p;
        GetComponentInChildren<Text>().text = p.Surname+" "+p.FirstName;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (FindObjectsOfType<PaperWithName>().ToList().Find(p=>p.Dragging))
        {
            return;
        }

        GameObject paper = Instantiate(Resources.Load("Prefabs/PaperWithName") as GameObject);
        paper.transform.SetParent(GameObject.FindGameObjectWithTag("GameCanvas").transform);
        paper.transform.localScale = Vector3.one;
        paper.GetComponent<PaperWithName>().Init(person);
    }
}
