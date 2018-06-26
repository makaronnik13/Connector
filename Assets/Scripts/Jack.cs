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
        /*
        if (Phone.Instance.TalkingPhone)
        {
            return;
        }
        GetComponent<IWireDraggReciewer>().StartDragWire(hab);
        */
    }

    public void OnClick()
    {
        if (!DemoCallsController.Instance.listeningCallPanel)
        {
            return;
        }
        else
        {
            GetComponent<IWireDraggReciewer>().DropWire(transform.GetChild(0));
        }
    }

    public void OnDrop()
    {
       
    }

    public void OnHover()
    {
        if (!DemoCallsController.Instance.listeningCallPanel || Phone.Instance.TalkingPhone)
        {
            return;
        }
        ConnectionLine.Instance.SetStart(DemoCallsController.Instance.listeningCallPanel.hab, DemoCallsController.Instance.listeningCallPanel.state.person);
    }

    public void OnUnhover()
    {
       
    }
}
