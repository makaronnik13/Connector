using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using Sirenix.OdinInspector;

[System.Serializable]
public class State: ScriptableObject
{
    public Person person;
    [HideInInspector]
	public int day = 1;
    [HideInInspector]
    public int minute = 0;
    [HideInInspector]
    public float waitingTime = 5;
    [HideInInspector]
    public float canBeDisconnectedAfter = 10000;


    [Range(0,1)]
    public float WrongConnectionWarningChance = 0;
	[Range(0,1)]
	public float DropWarningChance = 0;
	[Range(0,1)]
	public float DisconnectWarningChance = 1;

	public float TalkingTime = 15;
	public Dialog monolog;


    public virtual List<Person> secondPersons()
    {
        return null;
    }

    public virtual Dialog StateDialog(int path)
    {
        return null;
    }

}
