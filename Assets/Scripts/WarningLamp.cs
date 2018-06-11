using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLamp : Singleton<WarningLamp> {

    public enum WarningLampState
    {
        defaultLamp,
		yellowLamp,
        warningLamp
    }

	public void SetState(WarningLampState state)
    {
        if (!Tutorial.Instance.finished)
        {
            return;
        }
        switch (state)
        {
            case WarningLampState.defaultLamp:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case WarningLampState.warningLamp:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
			case WarningLampState.yellowLamp:
				GetComponent<SpriteRenderer>().color = Color.yellow;
				break;
        }
    }
}
