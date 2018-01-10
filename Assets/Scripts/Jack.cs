using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IWireDraggReciewer
{
    void StartDragWire(RectTransform tr);
    void DropWire(RectTransform endTransform);
}

public class Jack : MonoBehaviour, IDragHandler, IDropHandler
{

    public void OnDrag(PointerEventData eventData)
    {
        GetComponentInParent<IWireDraggReciewer>().StartDragWire(GetComponent<RectTransform>());
    }

    public void OnDrop(PointerEventData eventData)
    {
        GetComponentInParent<IWireDraggReciewer>().DropWire(GetComponent<RectTransform>());
    }
}
