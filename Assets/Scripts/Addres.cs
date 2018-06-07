using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Addres : MonoBehaviour, IDragHandler
{
    private Person person;

	public void Init(Person p)
    {
        person = p;
		if (!p.Service) 
		{
			GetComponent<TextMeshProUGUI> ().text = p.Surname + " " + p.FirstName;
		} else 
		{
			GetComponentInChildren<TextMeshProUGUI> ().text = p.PersonName;
		}
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (FindObjectsOfType<PaperWithName>().ToList().Find(p=>p.Dragging))
        {
            return;
        }

        GameObject paper = Instantiate(Resources.Load("Prefabs/PaperWithName") as GameObject);
        paper.transform.localScale = Vector3.one * 0.02f; ;
        paper.GetComponent<PaperWithName>().Init(person);
    }

	public void Click ()
	{
		FindObjectOfType<HabsParent> ().AddPerson (person);
	}
}
