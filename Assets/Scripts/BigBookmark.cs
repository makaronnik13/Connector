using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigBookmark : MonoBehaviour {

    public enum BigBookmarkType
    {
        Abonents,
        Services
    }

    public BigBookmarkType BookmarkType;

    private Color baseColor;

    private void Start()
    {
        baseColor = GetComponent<Image>().color;
        if (BookmarkType == BigBookmarkType.Abonents)
        {
            Click();
        }
    }

    public void Click()
    {
        foreach (BigBookmark bm in FindObjectsOfType<BigBookmark>())
        {
            bm.Dehighlight();
        }
        GetComponent<Image>().color = Color.white;
        GetComponentInParent<AddresBook>().ClickBigBookmark(BookmarkType);
    }

    public void Dehighlight()
    {
        GetComponent<Image>().color = baseColor;
    }
}
