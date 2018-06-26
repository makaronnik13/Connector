using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject menu;

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
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
