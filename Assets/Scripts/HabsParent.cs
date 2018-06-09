using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabsParent : MonoBehaviour {

	HabField EmptyHab
	{
		get
		{
			foreach(HabField hf in GetComponentsInChildren<HabField>())
			{
				if(hf.Person == null)
				{
					return hf;
				}
			}

			return null;
		}
	}

	public bool AddPerson(Person person)
	{
		if(!EmptyHab)
		{
			return false;
		}

		GameObject paperGo = Instantiate(Resources.Load("Prefabs/PaperWithName") as GameObject);
		PaperWithName paper = paperGo.GetComponent<PaperWithName>();
		paper.Init (person);
		paper.Dragging = false;
		paper.transform.SetParent(EmptyHab.transform.GetComponentInChildren<Glass>().transform.GetChild(0));
		paper.transform.localPosition = Vector3.zero;
		paper.transform.localScale = Vector3.one*0.9f;
		paper.CancelDestroy();
		EmptyHab.Person = paper.person;

		return true;
	}
}
