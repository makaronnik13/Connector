using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickAction : MonoBehaviour
{
    public UnityEvent action;
    public KeyCode key;

	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(key)))
        {
            action.Invoke();
        }
	}
}
