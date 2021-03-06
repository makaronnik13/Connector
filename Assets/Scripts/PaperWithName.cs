﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaperWithName : MonoBehaviour {

    public Person person;
    public bool Dragging;
	public HabField startField;

    private Canvas paperCanvas;
    private Canvas PaperCanvas
    {
        get
        {
            if (paperCanvas == null)
            {
                paperCanvas = GetComponent<Canvas>();
            }
            return paperCanvas;
        }
    }

	public void Init(Person p, HabField startHab = null)
    {
		startField = startHab;
        Dragging = true;
        person = p;
		if (!person.Service) {
			GetComponentInChildren<TextMeshProUGUI> ().text = p.Surname  + " " + p.FirstName[0];
		} else 
		{
			GetComponentInChildren<TextMeshProUGUI> ().text = p.PersonName;
		}
    }

    private void Update()
    {
        if (Dragging)
        {

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
          
            if (Input.GetMouseButtonUp(0))
            {
                Invoke("DestroyPaper", 0.1f);
            }
        }
        if (transform.parent==null)
        {
            PaperCanvas.sortingOrder = 400;
        }
        else
        {
            PaperCanvas.sortingOrder = 2;
            transform.localRotation = Quaternion.identity;
        }
    }


    private void DestroyPaper()
    {
        Destroy(gameObject);
    }

    public void CancelDestroy()
    {
        CancelInvoke("DestroyPaper");
    }
}
