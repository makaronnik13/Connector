using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AddresBook : MonoBehaviour {

    List<Person> persons = new List<Person>();

    public Transform page1, page2;

    public GameObject AddresPrefab;


    private void Start()
    {
        persons = Resources.LoadAll<Person>("Persons").ToList();
    }

    public void Open()
    {
        GetComponent<Animator>().SetBool("Open", true);
        
        ShowPage(new char[3] {'А', 'Б', 'В'});
    }

    public void ShowPage(char[] chars)
    {
        foreach (Transform t in page1.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in page2.transform)
        {
            Destroy(t.gameObject);
        }

        List<Person> pagePersons = persons.FindAll(p => chars.Contains(p.Surname[0])).OrderBy(p=>p.Surname[0]).ToList();

        Debug.Log(pagePersons.Count);

        int v = Mathf.Min(13, pagePersons.Count);

        for (int i = 0; i<v;i++)
        {
            GameObject newAddres = Instantiate(AddresPrefab, page1);
            newAddres.transform.localScale = Vector3.one;
            newAddres.GetComponent<Addres>().Init(pagePersons[i]);
        }

        if (pagePersons.Count>13)
        {
            for (int i = 13; i < pagePersons.Count; i++)
            {
                GameObject newAddres = Instantiate(AddresPrefab, page2);
                newAddres.transform.localScale = Vector3.one;
                newAddres.GetComponent<Addres>().Init(pagePersons[i]);
            }
        }
    }

    public void Close()
    {
        GetComponent<Animator>().SetBool("Open", false);
       
    }

    public void Switch()
    {
        if (GetComponent<Animator>().GetBool("Open"))
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
