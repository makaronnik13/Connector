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

    public Transform services1, services2;

    public void ClickBigBookmark(BigBookmark.BigBookmarkType bookmarkType)
    {
        switch (bookmarkType)
        {
            case BigBookmark.BigBookmarkType.Abonents:
                ShowAbonents();
                break;
            case BigBookmark.BigBookmarkType.Services:
                ShowServices();
                break;
        }
    }

    public Transform page1, page2;

    public GameObject AbonentsThings, ServicesThings;

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

        foreach (Transform t in services1)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in services2)
        {
            t.gameObject.SetActive(false);
        }


        lastChars = chars;

        bool isDigitPresent = chars.Any(c => char.IsDigit(c));

        AbonentsThings.SetActive(!isDigitPresent);
        ServicesThings.SetActive(isDigitPresent);

        
        lastAlfChars = chars;
       
        foreach (Transform t in page1.transform)
        {
            if (t!=services1)
            {
                Destroy(t.gameObject);
            }
            
        }
        foreach (Transform t in page2.transform)
        {
            if (t!=services2)
            {
                Destroy(t.gameObject);
            }
        }

        List<Person> pagePersons = new List<Person>();

		if (isDigitPresent) 
		{
            int page = int.Parse(chars[0].ToString());

            services1.GetChild(page).gameObject.SetActive(true);
            services2.GetChild(page).gameObject.SetActive(true);
        } 
		else 
		{

			pagePersons = persons.FindAll(p => chars.Contains(p.Surname[0])).OrderBy(p=>p.Surname[0]).ToList();
		}
        

		pagePersons.RemoveAll (pp=>pp.hideInBook);


        for (int i = 0; i< pagePersons.Count; i++)
        {
            GameObject newAddres = Instantiate(AddresPrefab);
            if (i < 7)
            {
                newAddres.transform.SetParent(page1);
            }
            else
            {
                newAddres.transform.SetParent(page2);
            }
            newAddres.transform.localScale = Vector3.one;
            newAddres.GetComponent<Addres>().Init(pagePersons[i]);
        }
    }



    public void OpenBook()
    {
        bookBlockCollider.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        bookPageCollider.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        SoundController.Instance.PlaySound(5);
        GetComponent<Collider2D>().enabled = false;
        CameraController.Instance.SetCameraView(3);
        GetComponent<Animator>().SetTrigger("Open");
        if (lastChars == null)
        {
            lastChars = new char[3] { 'А', 'Б', 'В'};
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

    private void ShowServices()
    {
        ShowPage(new char[] { '.' });
        bookmarks.SetActive(false);
    }

    private void ShowAbonents()
    {
        bookmarks.SetActive(true);
        ShowPage(lastAlfChars);
    }

    public void DragAway()
    {
        foreach (PaperWithName p in FindObjectsOfType<PaperWithName>())
        {
            if (p.Dragging)
            {
                CloseBook();
            }
        }
    }
}
