using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteButton : MonoBehaviour, ISpriteInputHandler
{
    public UnityEvent OnClickCallback;
    public UnityEvent OnHooverCallback;
    public UnityEvent OnUnHooverCallback;
    public UnityEvent OnDropCallback;
    public UnityEvent OnStartDraggingCallback;

    private bool dragging;

    public Sprite hooveredSprite;

    [SerializeField]
    private bool interactable;
    public bool Interactable
    {
        get
        {
            return interactable;
        }
        set
        {
            interactable = value;
            if (interactable)
            {
                InputController.Instance.AddListener(this);
            }
            else
            {
                InputController.Instance.RemoveListener(this);
            }
        }
    }

    private SpriteRenderer sRenderer;
    private Sprite defaultSprite;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = sRenderer.sprite;

        if (Interactable)
        {
            InputController.Instance.AddListener(this);
        }
      
    }

    public void OnDrag(Vector2 delta)
    {
        if (!dragging)
        {
            OnStartDraggingCallback.Invoke();
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    public void OnDrop()
    {
        OnDropCallback.Invoke();
    }

    public void OnHover()
    {
        if (sRenderer)
        {
            sRenderer.sprite = hooveredSprite;
        }
        OnHooverCallback.Invoke();
    }

    public void OnUnhover()
    {
        if (sRenderer)
        {
            sRenderer.sprite = defaultSprite;
        }
       OnUnHooverCallback.Invoke();
    }

    public void OnClick()
    {
        OnClickCallback.Invoke();
    }

}
