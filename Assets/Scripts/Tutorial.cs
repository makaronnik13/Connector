using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : Singleton<Tutorial>
{

    public DemoCallsController callsController;
    public StorryState tutorialState;
    public ShowingItems[] showingItems;

    private bool finished = false;
    public bool DropEnabled = false;

    [System.Serializable]
    public struct ShowingItems
    {
        public GameObject[] objects;
    }

    private void Start()
    {
        DialogController.Instance.OnTypingFinished += DialogFinished;
        DialogController.Instance.PlayMonolog(tutorialState);
    }

    private void DialogFinished()
    {
        DialogController.Instance.OnTypingFinished -= DialogFinished;
        DialogController.Instance.OnTypingFinished += DialogFinished2;


        foreach (SpriteRenderer sr in FindObjectsOfType<SpriteRenderer>())
        {
            sr.color = new Color(0.3f, 0.3f, 0.3f, 1);
        }

        foreach (GameObject go in showingItems[0].objects)
        {
            Collider2D collider = go.GetComponent<Collider2D>();
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
           
            if (sr)
            {
                sr.color = new Color(1f, 1f, 1f, 1);
            }
           
            if (collider)
            {
                collider.enabled = true;
            }
           
        }
    }

    private void DialogFinished2()
    {
        DialogController.Instance.OnTypingFinished -= DialogFinished2;

        /*
        DropEnabled = true;
        foreach (GameObject go in showingItems[2].objects)
        {
            Collider2D collider = go.GetComponent<Collider2D>();
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();

            if (sr)
            {
                sr.color = new Color(1f, 1f, 1f, 1);
            }

            if (collider)
            {
                collider.enabled = true;
            }

        }
        */
    }

    public void CloseManual()
    {
        if (!finished)
        {
            DialogFinished2();
            DropEnabled = false;
            foreach (SpriteRenderer sr in FindObjectsOfType<SpriteRenderer>())
            {
                sr.color = new Color(1f, 1f, 1f, 1);
            }

            callsController.StartCalls();

            DialogController.Instance.HideDialog();


            foreach (GameObject go in showingItems[1].objects)
            {
                Collider2D sb = go.GetComponent<Collider2D>();
                if (sb)
                {
                    sb.enabled = true;
                }
            }

            finished = true;
        }
    }
}
