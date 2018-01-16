using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[System.Serializable]
public class State: ScriptableObject
{
	public int day = 1;
	public int minute = 0;
	public float waitingTime = 30;
	public float canBeDisconnectedAfter = 10000;
	[Range(0,1)]
	public float badChance = 1;

	public Dialog monolog;

    public Person person;

    public virtual Person secondPerson()
    {
        return null;
    }

    public virtual Dialog StateDialog(int path)
    {
        return null;
    }

}
