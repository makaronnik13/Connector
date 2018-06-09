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

    private Collider2D _collider2d;
    private Collider2D collider2d
    {
        get
        {
            if (!_collider2d)
            {
                _collider2d = GetComponent<Collider2D>();
            }
            return _collider2d;
        }
    }

    private bool dragging;
    private bool hovered = false;

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
    public Sprite disabledSprite;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = sRenderer.sprite;

        if (Interactable)
        {
            InputController.Instance.AddListener(this);
        }
    }

    void Update()
    {
        if (!collider2d.enabled && disabledSprite)
        {
            sRenderer.sprite = disabledSprite;
        }
        else
        {
            if (hovered)
            {
                sRenderer.sprite = hooveredSprite;
            }
            else
            {
                sRenderer.sprite = defaultSprite;
            }
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
        hovered = true;
        if (sRenderer)
        {
            sRenderer.sprite = hooveredSprite;
        }
        OnHooverCallback.Invoke();
    }

    public void OnUnhover()
    {
        hovered = false;
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
