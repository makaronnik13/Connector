using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookMark : MonoBehaviour {

    public char[] chars;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<Text>().text = chars[0] + "-" + chars[chars.Length-1];
	}
	
	public void ShowPage()
    {
        GetComponentInParent<AddresBook>().ShowPage(chars);
    }
}
