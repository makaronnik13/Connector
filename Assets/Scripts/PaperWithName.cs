using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperWithName : MonoBehaviour {

    public Person person;
    public bool Dragging;
	public HabField startField;

    private Canvas parentCanvas
    {
        get
        {
            return GetComponentInParent<Canvas>();
        }
    }

	public void Init(Person p, HabField startHab = null)
    {
		startField = startHab;
        Dragging = true;
        person = p;
		if (!person.Service) {
			GetComponentInChildren<Text> ().text = p.Surname [0] + ". " + p.FirstName;
		} else 
		{
			GetComponentInChildren<Text> ().text = p.PersonName;
		}
    }

    private void Update()
    {
        if (Dragging)
        {
            Vector2 pos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
            transform.position = parentCanvas.transform.TransformPoint(pos);

            if (Input.GetMouseButtonUp(0))
            {
                Invoke("DestroyPaper", 0.1f);
            }
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
