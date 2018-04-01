using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IWireDraggReciewer
{
    void StartDragWire(RectTransform tr);
    void DropWire(RectTransform endTransform);
}

public class Jack : MonoBehaviour, IDragHandler
{

    public void OnDrag(PointerEventData eventData)
    {
		if(Phone.Instance.TalkingPhone)
		{
			return;
		}
		GetComponent<IWireDraggReciewer>().StartDragWire(transform.GetChild(0).GetComponent<RectTransform>());
    }
}
