﻿using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "GameModel/Balance")]
public class Balance : ScriptableObject
{
    public DayPair[] days = new DayPair[0];
    public float charactersPerSecond = 4;
    [System.Serializable]
    public class DayPair
    {
        public int day = 1;
        public AnimationCurve curve;
    }

}