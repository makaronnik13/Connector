using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookMark : MonoBehaviour {

    public char[] chars;
	
	public void ShowPage()
    {
        Debug.Log(gameObject);
        GetComponentInParent<AddresBook>().ShowPage(chars);
    }
}
