using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailsCounter : Singleton<FailsCounter> {

    private int _fails = 0;

    public Action OnGameOver = ()=> { };

	public void AddFail()
    {
        transform.GetChild(_fails).gameObject.SetActive(true);
        _fails++;
        if (_fails == transform.childCount)
        {
            OnGameOver();
        }
    }
}
