using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public void Start()
	{
		Slider[] sliders = GetComponentsInChildren<Slider> ();
		sliders [0].value = SoundController.Instance.MusicVolume;
			sliders [1].value = SoundController.Instance.SoundVolume;
	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void SetMusic(Slider slider)
	{
		SoundController.Instance.SetMusic (slider.value);
	}

	public void SetSounds(Slider slider)
	{
		SoundController.Instance.SetSound (slider.value);
	}

	public void ChangeMenuState()
	{
		GetComponent<Animator> ().SetTrigger ("State");
	}
}
