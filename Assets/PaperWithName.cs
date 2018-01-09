using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperWithName : MonoBehaviour {

    public Person person;
    public bool Dragging;

    private Canvas parentCanvas
    {
        get
        {
            return GetComponentInParent<Canvas>();
        }
    }

	public void Init(Person p)
    {
        Dragging = true;
        person = p;
        GetComponentInChildren<Text>().text =  p.Surname[0]+". " + p.FirstName;
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
