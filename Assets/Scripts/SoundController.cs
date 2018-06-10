using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundController : Singleton<SoundController> {

	private List<AudioSource> sources = new List<AudioSource> ();

    public AudioClip[] specialClips;
    public AudioClip[] backgroundClips;
	private Queue<AudioClip> clips;
	private AudioClip currentClip;
    public float InOutTime = 1;

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

    public void PlaySound(int i)
    {
        PlaySound(specialClips[i]);
    }

	public void PlaySound(AudioClip clip)
	{
		sources [1].PlayOneShot (clip);
	}

	private IEnumerator PlayMusic()
	{
        while (true)
        {

            if (currentClip == null)
            {
                Debug.Log("play");
                sources[0].volume = 0;
                StartCoroutine(FadeIn(sources[0], 2));
                currentClip = clips.Dequeue();
                sources[0].clip = currentClip;
                sources[0].Play();
                clips.Enqueue(currentClip);
            }

            if (sources[0].time == sources[0].clip.length-InOutTime)
            {
                StartCoroutine(FadeOut(sources[0],2));
            }
            if (sources[0].time >= sources[0].clip.length)
            {
                currentClip = null;
            }
            yield return null;
        }
	}

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -=  Time.deltaTime / FadeTime;

            yield return null;
        }
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume < 1)
        {
            Debug.Log(audioSource.volume);
            audioSource.volume +=  Time.deltaTime / FadeTime;

            yield return null;
        }
    }
}
