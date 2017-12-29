using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConnectionPoint : MonoBehaviour, IDropHandler, IDragHandler
{
    private enum PointState
    {
        Inactive,
        Pinging,
        Recieving
    }
    private PointState pointState = PointState.Inactive;
    private float Radius
    {
        get
        {
            return r;
        }
    }
	private float Horizontal;
	private float Vertiacal;
    private Material material;
    private Button button;
    private Button Button
    {
        get
        {
            if (!button)
            {
                button = GetComponent<Button>();
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
            if (!img)
            {
                img = GetComponent<Image>();
            }
            return img;
        }
    }

    private float r = 267f;
	public State state;

    #region LifeCycle
    void Start()
	{
		Rotator.Instance.OnAngleChanged += OnPlanetRotationChanged;
		DialogController.Instance.onDialogFinished += DialogFinished;
		GetComponent<Button> ().onClick.AddListener (PointClicked);
	}
    public void Init(State state)
    {
        this.state = state;
        Horizontal = state.Horizontal;
        Vertiacal = state.Vertical;
        pointState = PointState.Pinging;

        material = new Material(GetComponent<Image>().material);
        GetComponent<Image>().material = material;


        material.SetColor("_Color", Color.red);
        material.SetFloat("_frequency", 0.3f);
        material.SetFloat("_speed", 0.15f);
    }
    #endregion

    private void DialogFinished(State s1, State s2)
	{
		if(s1 == state)
		{
			if (s2 == null) {
				pointState = PointState.Recieving;
               material.SetColor("_Color", Color.yellow);
               material.SetFloat("_frequency", 0.15f);
                material.SetFloat("_speed", 0.05f);
            } else 
			{
                Rotator.Instance.OnAngleChanged -= OnPlanetRotationChanged;
                DialogController.Instance.onDialogFinished -= DialogFinished;
                Destroy (gameObject);
			}
		}

        if (s2 == state)
        {
            if (s1 == null)
            {
                pointState = PointState.Recieving;
               material.SetColor("_Color", Color.yellow);
                material.SetFloat("_frequency", 0.15f);
               material.SetFloat("_speed", 0.05f);
            }
            else
            {
                Rotator.Instance.OnAngleChanged -= OnPlanetRotationChanged;
                DialogController.Instance.onDialogFinished -= DialogFinished;
                Destroy(gameObject);
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
			if (pointState == PointState.Pinging)
            {
				DialogController.Instance.Talk (state);
			}
		}
	}

    #region Callbacks
    public void OnDrop(PointerEventData eventData)
    {
  
        if (ConnectionLine.Instance.From == state || pointState!= PointState.Recieving)
        {
            return;
        }

 
        DialogController.Instance.Talk (ConnectionLine.Instance.From , state);  
    }
    public void OnDrag(PointerEventData eventData)
    {
      
        if (pointState == PointState.Recieving)
        {
            ConnectionLine.Instance.SetStart(GetComponent<RectTransform>());
        }
    }
    #endregion
}
