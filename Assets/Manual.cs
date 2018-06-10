using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour {

	public void OpenManual()
    {
        SoundController.Instance.PlaySound(3);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("Open");
        CameraController.Instance.SetCameraView(2);
    }

    public void CloseManual()
    {
        SoundController.Instance.PlaySound(3);
        transform.GetChild(0).GetComponent<ClickAction>().enabled = false;
        Tutorial.Instance.CloseManual();
        CameraController.Instance.SetCameraView(0);
        GetComponent<Animator>().SetTrigger("Close");
    }

    public void CloseManualFinished()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void OpenManualFinished()
    {
        transform.GetChild(0).GetComponent<ClickAction>().enabled = true;
    }
}
