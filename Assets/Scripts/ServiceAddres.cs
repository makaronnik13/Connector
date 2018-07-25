using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ServiceAddres : MonoBehaviour, IDragHandler
{
    public Person person;


    public void OnDrag(PointerEventData eventData)
    {
        if (FindObjectsOfType<PaperWithName>().ToList().Find(p => p.Dragging))
        {
            return;
        }

        GameObject paper = Instantiate(Resources.Load("Prefabs/PaperWithName") as GameObject);
        paper.transform.localScale = Vector3.one * 0.015f; ;
        paper.GetComponent<PaperWithName>().Init(person);
    }

    public void Click()
    {
        FindObjectOfType<HabsParent>().AddPerson(person);
        FindObjectOfType<AddresBook>().CloseBook();
    }

    public void OnHover()
    {
        GetComponentInChildren<Image>().color = Color.yellow;
    }

    public void OnUnHover()
    {
        GetComponentInChildren<Image>().color = Color.white;
    }
}
