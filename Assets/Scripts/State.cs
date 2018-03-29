using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[System.Serializable]
public class State: ScriptableObject
{
    public Person person;
    [HideInInspector]
	public int day = 1;
    [HideInInspector]
    public int minute = 0;
    [HideInInspector]
    public float waitingTime = 30;
    [HideInInspector]
    public float canBeDisconnectedAfter = 10000;
    //[Range(0,1)]
    [HideInInspector]
    public float badChance = 1;

	public Dialog monolog;


    public virtual Person secondPerson()
    {
        return null;
    }

    public virtual Dialog StateDialog(int path)
    {
        return null;
    }

}
