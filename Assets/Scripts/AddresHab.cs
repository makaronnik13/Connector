using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddresHab : MonoBehaviour
{
    public Transform habtransform;
   public void Connect()
    {
        if (!DemoCallsController.Instance.listeningCallPanel)
        {
            return;
        }
        else
        {
            GetComponentInParent<IWireDraggReciewer>().DropWire(habtransform);
        }
    }



    public void Hover()
    {
        if (!DemoCallsController.Instance.listeningCallPanel)
        {
            return;
        }
        ConnectionLine.Instance.SetStart(DemoCallsController.Instance.listeningCallPanel.hab, DemoCallsController.Instance.listeningCallPanel.state.person);
    }

}
