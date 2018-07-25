using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncher : MonoBehaviour {

    public void Load()
    {
        StartCoroutine(LoadCor());
    }

    private IEnumerator LoadCor()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
