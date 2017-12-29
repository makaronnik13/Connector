using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TascsManager : Singleton<TascsManager> {

	public GameObject taskPrefab;

	void Start()
	{
		DialogController.Instance.onDialogFinished += DialogFinished;
	}

	public void AddTask(State state)
	{
		GameObject newTask = Instantiate (taskPrefab, transform);
		newTask.transform.localScale = Vector3.one;
		newTask.GetComponent<Task> ().Init (state);
	}

	public void RemoveTask(State state)
	{
		foreach(Task task in GetComponentsInChildren<Task>())
		{
			if(task.state == state)
			{
				Destroy (task.gameObject);
			}
		}
	}

	private void DialogFinished(State firstState, State secondState)
	{
		if(secondState == null && firstState!=null)
		{
			AddTask (firstState);
		}
	}
}
