using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddresBook : MonoBehaviour {

    public Collider2D bookPageCollider, bookBlockCollider;
    public GameObject BookCanvas;
    private List<Person> persons = new List<Person>();
	private List<Person> services = new List<Person>();
    public Transform page;

    //public Image ServicesMark, AbonentsMark;
    

    public GameObject bookmarks;

    public GameObject AddresPrefab;

    private char[] lastChars;
    private char[] lastAlfChars;

    private void Start()
    {
        persons = Resources.LoadAll<Person>("Persons/Simple").ToList();
		services = (Resources.LoadAll<Person>("Persons/Special").ToList());
    }


    public void ShowPage(char[] chars)
    {
        SoundController.Instance.PlaySound(5);

        lastChars = chars;

        if (!chars.Contains('.'))
        {
            lastAlfChars = chars;
        }
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



    public void OpenBook()
    {
        SoundController.Instance.PlaySound(5);
        GetComponent<Collider2D>().enabled = false;
        CameraController.Instance.SetCameraView(3);
        GetComponent<Animator>().SetTrigger("Open");
        if (lastChars == null)
        {
            lastChars = new char[4] { 'А', 'Б', 'В', 'Г' };
        }
        ShowPage(lastChars);
    }

    public void CloseBook()
    {
        GetComponent<Animator>().SetTrigger("Close");
        CameraController.Instance.SetCameraView(0);
        SoundController.Instance.PlaySound(5);
        BookCanvas.SetActive(false);
        bookBlockCollider.enabled = false;
        bookPageCollider.enabled = false;

    }

    public void CloseBookFinished()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void OpenBookFinished()
    {
        bookBlockCollider.enabled = true;
        bookPageCollider.enabled = true;
    }

    public void ShowServices()
    {
        ShowPage(new char[] { '.' });
        bookmarks.SetActive(false);
    }

    public void ShowAbonents()
    {
        bookmarks.SetActive(true);
        ShowPage(lastAlfChars);
    }
}
