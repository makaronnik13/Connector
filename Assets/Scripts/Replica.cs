using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Replica 
{
    [HideInInspector]
	public Dialog.Person person = Dialog.Person.FirstPerson;
	public string text;
}
