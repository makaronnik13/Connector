using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	private bool hidden = true;
    public GameObject Panel;

	public void SwitchMap()
	{
		if (hidden) {
			ShowMap ();
		} else {
			HideMap ();
		}
	}

	public void ShowMap()
	{
        StopAllCoroutines();
        StartCoroutine(SetPanel(0.7f, false));
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
		hidden = false;
		GetComponent<Animator> ().SetBool ("Open", true);
	}

    private IEnumerator SetPanel(float delay, bool v)
    {
        yield return new WaitForSeconds(delay);
        foreach (SpriteRenderer sr in Panel.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = v;
        }
    }

    public void HideMap()
	{
        StopAllCoroutines();
        StartCoroutine(SetPanel(0.1f, true));
        GetComponent<Collider2D>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
        hidden = true;
		GetComponent<Animator> ().SetBool ("Open", false);
	}
}
