using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBlock : MonoBehaviour {


	public void ShowLine()
    {
        if (!DemoCallsController.Instance.listeningCallPanel || Phone.Instance.TalkingPhone)
        {
            return;
        }
        ConnectionLine.Instance.SetStart(DemoCallsController.Instance.listeningCallPanel.hab, DemoCallsController.Instance.listeningCallPanel.state.person);
    }

    public void HideLine()
    {
        ConnectionLine.Instance.Hide();
    }

}
