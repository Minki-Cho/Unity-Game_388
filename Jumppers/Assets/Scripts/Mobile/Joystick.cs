// File name   : Joystick.cs
// Author(s)   : Minki Cho
// Copyright  : Copyright(C) 2025 DigiPen Institute of Technology
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    public Vector2 inputVector;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out pos);

        pos /= background.sizeDelta;

        inputVector = pos * 2f;
        inputVector = inputVector.magnitude > 1 ? inputVector.normalized : inputVector;

        handle.anchoredPosition = inputVector * (background.sizeDelta / 3f);
    }

    public float Horizontal() => inputVector.x;
    public float Vertical() => inputVector.y;
}
