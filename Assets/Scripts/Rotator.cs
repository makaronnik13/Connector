using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotator : Singleton<Rotator> {

    public float speed = 1;
	public Action<float> OnAngleChanged = (float v)=>{};

	public bool rotating = true;

	// Update is called once per frame
	void Update () {
		if(rotating)
		{
        	transform.Rotate(Vector3.up, speed*Time.deltaTime);
			OnAngleChanged.Invoke (transform.rotation.eulerAngles.y);
		}
	}
}
