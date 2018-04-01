using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	private bool hidden = true;

	public void SwitchMap()
	{
		if (hidden) {
			ShowMap ();
		} else {
			HideMap ();
		}
	}

	public void ShowMap()
	{
		hidden = false;
		GetComponent<Animator> ().SetBool ("Open", true);
	}

	public void HideMap()
	{
		hidden = true;
		GetComponent<Animator> ().SetBool ("Open", false);
	}
}
