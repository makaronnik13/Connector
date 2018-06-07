using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundController : Singleton<SoundController> {

	private List<AudioSource> sources = new List<AudioSource> (); 

	public AudioClip[] backgroundClips;
	private Queue<AudioClip> clips;
	private AudioClip currentClip;

	public float SoundVolume
	{
		get
		{
			return sources [1].volume;
		}
	}
	public float MusicVolume
	{
		get
		{
			return sources [0].volume;
		}
	}

	// Use this for initialization
	void Awake () {
		sources = GetComponents<AudioSource> ().ToList();

		if(!PlayerPrefs.HasKey("Sound"))
		{
			SetSound (1);
			SetMusic (1);
		}

		SetSound (PlayerPrefs.GetFloat("Sound"));
		SetMusic (PlayerPrefs.GetFloat("Music"));
		clips = new Queue<AudioClip> (backgroundClips);
		StartCoroutine (PlayMusic());
	}

	public void SetSound(float value)
	{
		sources [1].volume = value;
		PlayerPrefs.SetFloat ("Sound", value);
	}

	public void SetMusic(float value)
	{
		sources [0].volume = value;
		PlayerPrefs.SetFloat ("Music", value);
	}

	public void PlaySound(AudioClip clip)
	{
		sources [1].PlayOneShot (clip);
	}

	private IEnumerator PlayMusic()
	{
		if (currentClip == null)
        {
			currentClip = clips.Dequeue();
			sources [0].clip = currentClip;
			sources [0].Play ();
			clips.Enqueue (currentClip);
			sources [0].volume = 0;
		}

		while (sources [0].clip!=null) {
			if(sources[0].time<6)
			{
				sources [0].volume = Mathf.Lerp(sources [0].volume, PlayerPrefs.GetFloat ("Music")*1, Time.deltaTime*5);
			}
			if(sources[0].time>sources[0].clip.length-6)
			{
				sources [0].volume = Mathf.Lerp(sources [0].volume, 0, Time.deltaTime*5);
			}
			if(sources[0].time>=sources[0].clip.length)
			{
				sources [0].clip = null;
			}
			yield return new WaitForSeconds(0.1f);
		}
		StartCoroutine (PlayMusic());
		yield return null;
	}
}
