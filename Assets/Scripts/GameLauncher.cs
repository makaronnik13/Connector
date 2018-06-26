using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Load());
	}

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}
