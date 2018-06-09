﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLamp : Singleton<WarningLamp> {

    public enum WarningLampState
    {
        defaultLamp,
        warningLamp
    }

	public void SetState(WarningLampState state)
    {
        switch (state)
        {
            case WarningLampState.defaultLamp:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case WarningLampState.warningLamp:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
    }
}
