using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;

public class PersonPanel : MonoBehaviour {

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

	public void Show(Sprite sprite, string text)
	{
		writer.Stop ();
		img.sprite = sprite;
		writer.initialText = text;
		writer.Start();
		Show ();
	}

	public void Show()
	{
		Animator.SetBool ("Showing", true);
	}

	public void Hide()
	{
		Animator.SetBool ("Showing", false);
	}
}
