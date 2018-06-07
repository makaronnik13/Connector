using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public DemoCallsController callsController;
    public StorryState tutorialState;
    public ShowingItems[] showingItems;

    private bool finished = false;

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

        foreach (Collider2D collider in FindObjectsOfType<Collider2D>())
        {
            collider.enabled = false;
        }

        foreach (SpriteRenderer sr in FindObjectsOfType<SpriteRenderer>())
        {
            sr.color = new Color(0.3f, 0.3f, 0.3f, 1);
        }

        foreach (GameObject go in showingItems[0].objects)
        {
            Collider2D collider = go.GetComponent<Collider2D>();
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            SpriteButton sb = go.GetComponent<SpriteButton>();
            if (sr)
            {
                sr.color = new Color(1f, 1f, 1f, 1);
            }
           
            if (collider)
            {
                collider.enabled = true;
            }
            if (sb)
            {
                sb.Interactable = true;
            }
        }
    }


    public void CloseManual()
    {
        if (!finished)
        {
            foreach (Collider2D collider in FindObjectsOfType<Collider2D>())
            {
                collider.enabled = true;
            }

            foreach (SpriteRenderer sr in FindObjectsOfType<SpriteRenderer>())
            {
                sr.color = new Color(1f, 1f, 1f, 1);
            }

            callsController.StartCalls();

            DialogController.Instance.HideDialog();


            foreach (GameObject go in showingItems[1].objects)
            {
                SpriteButton sb = go.GetComponent<SpriteButton>();   
                if (sb)
                {
                    sb.Interactable = true;
                }
            }

            finished = true;
        }
    }
}
