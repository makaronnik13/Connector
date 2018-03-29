using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombinationLink :Link
{
    public Person person;
    [HideInInspector]
    public Dialog dialog;
}
