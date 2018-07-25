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
			GetComponentInChildren<TextMeshProUGUI> ().text = p.Surname + " " + p.FirstName;
            for (int i = 0; i<34- GetComponentInChildren<TextMeshProUGUI>().text.Length;i++)
            {
                GetComponentInChildren<TextMeshProUGUI>().text += "-";
            }

            GetComponentInChildren<TextMeshProUGUI>().text += p.Number;


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
        paper.transform.localScale = Vector3.one * 0.015f; ;
        paper.GetComponent<PaperWithName>().Init(person);
    }

	public void Click ()
	{
		FindObjectOfType<HabsParent> ().AddPerson (person);
        FindObjectOfType<AddresBook>().CloseBook();
	}

    public void OnHover()
    {
        GetComponentInChildren<Image>().enabled = true;
    }

    public void OnUnHover()
    {
        GetComponentInChildren<Image>().enabled = false;
    }
}
