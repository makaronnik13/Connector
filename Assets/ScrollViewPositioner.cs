using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewPositioner : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(!GetComponent<Image>().raycastTarget)
		{
		Canvas.ForceUpdateCanvases();
		GetComponent<ScrollRect>().verticalNormalizedPosition= 0f;	
		}
	}

	void Start()
	{
		GetComponentInChildren<Typewriter> ().OnComplete += TypingComplete;
	}

	private void TypingComplete()
	{
		GetComponent<Image>().raycastTarget = true;
	}

	public void Write(string initial, string[] additional)
	{
		GetComponent<Image> ().raycastTarget = false;
		GetComponentInChildren<Typewriter>().Write(initial, additional);
	}
}
