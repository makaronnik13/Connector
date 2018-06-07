using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : Singleton<InputController> {

    private List<ISpriteInputHandler> listeners = new List<ISpriteInputHandler>();
    private Vector2 lastMousePosition;
    private ISpriteInputHandler hovered;

    public void AddListener(ISpriteInputHandler handler)
    {
        StartCoroutine(AddListenerC(handler));
    }

    private IEnumerator AddListenerC(ISpriteInputHandler handler)
    {
        yield return null;
        if (!listeners.Contains(handler))
        {
            listeners.Add(handler);
        }
    }

    public void RemoveListener(ISpriteInputHandler handler)
    {
        StartCoroutine(RemoveListenerC(handler));
        
    }

    private IEnumerator RemoveListenerC(ISpriteInputHandler handler)
    {
        yield return null;
        if (listeners.Contains(handler))
        {
            listeners.Remove(handler);
        }
    }

    void Update()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject lastHitedObj = null;
            if (hovered!=null)
            {
                lastHitedObj = ((Component)hovered).gameObject;
            }

            if (hit.collider.gameObject!=lastHitedObj)
            {
                if (hovered!=null)
                {
                    hovered.OnUnhover();
                    hovered = null;
                }

                ISpriteInputHandler newHovered = hit.collider.GetComponent<ISpriteInputHandler>();
               if (newHovered != null)
                {
                    hovered = newHovered;
                   // Debug.Log(hovered);
                    hovered.OnHover();
                }
            }
            
        }
        else
        {
            if (hovered != null)
            {
                hovered.OnUnhover();
                hovered = null;
            }
        }

        if (hovered!=null)
        {
            if (Input.GetButtonDown("Fire1") && hovered != null)
            {
                hovered.OnClick();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                hovered.OnDrop();
            }

            if (Input.GetButton("Fire1") && lastMousePosition != mousePosition)
            {
                hovered.OnDrag(mousePosition - lastMousePosition);
            }
        }

        /*
        //Gets the world position of the mouse on the screen        
       
 
        List<ISpriteInputHandler> frameHovered = new List<ISpriteInputHandler>();

        foreach (ISpriteInputHandler handler in listeners)
        {
            bool overSprite = ((Component)handler).GetComponent<Collider2D>().bounds.Contains(mousePosition);

            if (overSprite)
            {
                frameHovered.Add(handler);
            }
        }

        if (frameHovered.Count == 0)
        {
            if (hovered!=null)
            {
                Debug.Log("u");
                hovered.OnUnhover();
            }
            hovered = null;
        }
        else
        {
            ISpriteInputHandler newHovered = frameHovered.OrderBy(l => ((Component)l).GetComponent<SpriteRenderer>().sortingLayerID).First();
            if (hovered!=newHovered)
            {
                if (hovered != null)
                {
                    hovered.OnUnhover();
                }

                hovered = newHovered;
                hovered.OnHover();
                //Debug.Log(hovered);
            }
           
        }
        

        foreach (ISpriteInputHandler handler in listeners)
        {
            bool overSprite = ((Component)handler).GetComponent<Collider2D>().bounds.Contains(mousePosition);

            if (overSprite)
            {
                if (Input.GetButtonDown("Fire1") && hovered!=null)
                {
                    Debug.Log("click");
                    hovered.OnClick();
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    handler.OnDrop();
                }

                if (Input.GetButton("Fire1") && lastMousePosition!=mousePosition)
                {
                    handler.OnDrag(mousePosition-lastMousePosition);
                }
            }
        }

    */

        lastMousePosition = mousePosition;
    }
}
