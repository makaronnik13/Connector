using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Glass : MonoBehaviour, IDragHandler, IDropHandler
{

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("!!!!!!");
        GetComponentInParent<HabField>().StartDragPaper();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("?????");
        GetComponentInParent<HabField>().DropPaper();
		GetComponentInParent<IWireDraggReciewer>().DropWire(transform.GetChild(1).GetComponent<RectTransform>());
    }
}
