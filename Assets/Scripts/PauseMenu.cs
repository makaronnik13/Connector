using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject menu;
    public GameObject failMenu;

    public void ShowPause()
    {
        menu.SetActive(true);
        //FindObjectOfType<DemoCallsController>().Pause();
        Phone.Instance.TalkingPhone = true;
    }

    public void HidePause()
    {
        menu.SetActive(false);
        Phone.Instance.TalkingPhone = false;
    }

    public void Reload()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    private void Start()
    {
        FailsCounter.Instance.OnGameOver += ShowGameOver;
    }

    private void ShowGameOver()
    {
        failMenu.gameObject.SetActive(true);
    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
