using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookMark : MonoBehaviour {

    public char[] chars;
    private Color baseColor;

    private void Start()
    {
        baseColor = GetComponent<Image>().color;
    }

    public void ShowPage()
    {
       // Debug.Log(gameObject);
        GetComponentInParent<AddresBook>().ShowPage(chars);
        foreach (BookMark bm in FindObjectsOfType<BookMark>())
        {
            bm.Dehighlight();
        }
        GetComponent<Image>().color = Color.yellow;
    }

    public void Dehighlight()
    {
        GetComponent<Image>().color = baseColor;
    }
}
