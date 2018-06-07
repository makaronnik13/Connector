using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Sirenix.OdinInspector;
using System;

public class PersonPanel : MonoBehaviour {

    private Sprite defaultSprite;

	public Sprite spriteTest;
	public string textTest;
	private Animator animator;
	private Animator Animator
	{
		get
		{
			if(!animator)
			{
				animator = GetComponent<Animator> ();
			}

			return animator;
		}
	}
	public Image img;
	public Typewriter writer;

    private void Start()
    {
        defaultSprite = img.sprite;
    }

    public void Show(Sprite sprite, string text)
	{
		img.sprite = sprite;
	}

	public void Hide()
	{
        img.sprite = defaultSprite;
        writer.Stop();
		if (writer.guiTextComponent) {
			writer.guiTextComponent.text = "";
		}
	}
}
