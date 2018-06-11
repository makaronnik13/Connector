using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsVisual : MonoBehaviour {

	public TextMeshProUGUI text;
	public Image img;

	public void Init(NewsVariant news)
	{
		text.text = news.text;
		img.sprite = news.img;
	}
}
