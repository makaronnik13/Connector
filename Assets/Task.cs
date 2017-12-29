using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour {

	public State state;

	public void Init(State state)
	{
		this.state = state;
		GetComponentInChildren<Text> ().text = state.StateName;
	}
}
