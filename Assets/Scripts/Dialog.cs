using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[Serializable]
public class Dialog  
{
	public enum Person
	{
		FirstPerson,
		SecondPerson
	}

	public AudioClip clip;

	public List<Replica> replics;
}
