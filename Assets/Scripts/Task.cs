using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Task : MonoBehaviour, IDragHandler, IDropHandler
{
	public State state;

	public void Init(State state)
	{
		this.state = state;
		GetComponentInChildren<Text> ().text = state.StateName;
        DialogController.Instance.onDialogFinished += DialogFinished;
	}
    private void DialogFinished(State arg1, State arg2)
    {
        if (arg1 == state || arg2 == state)
        {
            Destroy(gameObject);
        }
    }

    #region Callbacks
    public void OnDrag(PointerEventData eventData)
    {
        ConnectionLine.Instance.SetStart(GetComponent<RectTransform>());
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (ConnectionLine.Instance.From == state)
        {
            return;
        }

        DialogController.Instance.Talk(ConnectionLine.Instance.From, state);
    }
    #endregion
}
