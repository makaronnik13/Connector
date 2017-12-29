using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionPoint : MonoBehaviour {
	
	[Range(0, 360)]
	public float Horizontal;
	[Range(-90,90)]
	public float Vertiacal;
	private float Radius
	{
		get
		{
			return Screen.height * 0.47f;
		}
	}

	public State state;

	private bool taskActive = true;

	private Button button;
	private Button Button
	{
		get
		{
			if(!button)
			{
				button = GetComponent<Button> ();
			}
			return button;
		}
	}

	private bool visible;

	private Image img;
	private Image Img
	{
		get
		{
			if(!img)
			{
				img = GetComponent<Image> ();
			}
			return img;
		}
	}

	void Start()
	{
		Rotator.Instance.OnAngleChanged += OnPlanetRotationChanged;
		DialogController.Instance.onDialogFinished += DialogFinished;
		GetComponent<Button> ().onClick.AddListener (PointClicked);
	}

	private void DialogFinished(State s1, State s2)
	{
		if(s1 == state)
		{
			if (s2 == null) {
				taskActive = false;
			} else 
			{
				Destroy (gameObject);
			}
		}

	}

	private void OnPlanetRotationChanged(float angle)
	{
		float angleHorizontal = (angle - Horizontal)*Mathf.Deg2Rad;

		float a = angleHorizontal * Mathf.Rad2Deg;
		if(a<0)
		{
			a = 360 + a;
		}

		visible = (a > 90) && (a  < 275);
		Button.interactable = visible;
		Img.enabled = visible;

		float SectionRadius = Mathf.Cos (Vertiacal*Mathf.Deg2Rad)*Radius;

		//Debug.Log (Mathf.Abs(Mathf.Sin(angleHorizontal)));
		Img.color = Color.Lerp (Color.white, new Color(1,1,1,0), Mathf.Pow(Mathf.Abs(Mathf.Sin(angleHorizontal)), 30));
		transform.localPosition = new Vector3 (Mathf.Sin(angleHorizontal)*SectionRadius, Mathf.Sin(Vertiacal*Mathf.Deg2Rad)*Radius, transform.localPosition.z);
	}

	private void PointClicked()
	{
		if(Rotator.Instance.rotating)
		{
			if (taskActive) {
				DialogController.Instance.Talk (state);
				Rotator.Instance.rotating = false;
			} else 
			{
				//DialogController.Instance.Talk (TaskManager.Instance.SelectedTask ,state);
			}
		}
	}
}
