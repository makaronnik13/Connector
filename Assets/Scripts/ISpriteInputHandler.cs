using UnityEngine;

public interface ISpriteInputHandler
{
    void OnDrag(Vector2 delta);
    void OnClick();
    void OnDrop();
    void OnHover();
    void OnUnhover();
}