using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddresBook : MonoBehaviour {

    private List<Person> persons = new List<Person>();
	private List<Person> services = new List<Person>();
    public Transform page;

    public GameObject AddresPrefab;

    private char[] lastChars; 

    private void Start()
    {
        persons = Resources.LoadAll<Person>("Persons/Simple").ToList();
		services = (Resources.LoadAll<Person>("Persons/Special").ToList());
    }

    public void Open()
    {
        GetComponent<Animator>().SetTrigger("Open");

        if (lastChars==null)
        {
            lastChars = new char[3] { 'А', 'Б', 'В' };
        }
        ShowPage(lastChars);
    }

    public void ShowPage(char[] chars)
    {
        lastChars = chars;
        foreach (Transform t in page.transform)
        {
            Destroy(t.gameObject);
        }
       

		List<Person> pagePersons;

		if (chars.Contains ('.')) 
		{
			pagePersons = services;
		} 
		else 
		{
			pagePersons = persons.FindAll(p => chars.Contains(p.Surname[0])).OrderBy(p=>p.Surname[0]).ToList();
		}
        
		pagePersons.RemoveAll (pp=>pp.hideInBook);


        for (int i = 0; i< pagePersons.Count; i++)
        {
            GameObject newAddres = Instantiate(AddresPrefab, page);
            newAddres.transform.localScale = Vector3.one;
            newAddres.GetComponent<Addres>().Init(pagePersons[i]);
        }
    }

    public void Close()
    {
        GetComponent<Animator>().SetTrigger("Close");
        FindObjectOfType<CameraController>().SetCameraView(0);
    }
}
