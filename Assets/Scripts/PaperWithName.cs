using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
			GetComponentInChildren<TextMeshProUGUI> ().text = p.Surname [0] + ". " + p.FirstName;
		} else 
		{
			GetComponentInChildren<TextMeshProUGUI> ().text = p.PersonName;
		}
    }

    private void Update()
    {
        if (Dragging)
        {
            Vector2 pos;

            //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
          
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
