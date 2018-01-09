using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallPanel : MonoBehaviour, IWireDraggReciewer
{
    private enum CallPanelState
    {
        Off,
        Incoming,
        Waiting,
        Talking
    }

    private CallPanelState callPanelState = CallPanelState.Off;

    public State state;
    public Image lamp;

    public void DropWire(RectTransform endTransform)
    {
        if (state)
        {
            ConnectionLine.Instance.Drop(endTransform, state.person);
        }
    }

    public void StartDragWire(RectTransform tr)
    {
        if (state)
        {
            ConnectionLine.Instance.SetStart(tr, state.person);
        }
    }
}
