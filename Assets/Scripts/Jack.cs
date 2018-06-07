using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IWireDraggReciewer
{
    void StartDragWire(Transform tr);
    void DropWire(Transform endTransform);
}


public class Jack : MonoBehaviour, ISpriteInputHandler
{

    public bool Interactable;
    public Transform hab;

    void Start()
    {
        if (Interactable)
        {
            InputController.Instance.AddListener(this);
        }
    }

    public void OnDrag(Vector2 delta)
    {
        if (Phone.Instance.TalkingPhone)
        {
            return;
        }
        GetComponent<IWireDraggReciewer>().StartDragWire(hab);
    }

    public void OnClick()
    {
       
    }

    public void OnDrop()
    {
       
    }

    public void OnHover()
    {

    }

    public void OnUnhover()
    {
       
    }
}
