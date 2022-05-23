using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform Background;
    public JoyStickDirection JoyStickDirection = JoyStickDirection.Both;
    public RectTransform Handle;
    [Range(0, 2f)] public float HandleLimit = 1f;
    private Vector2 input = Vector2.zero;
    public float Vertical  { get { return input.y; } }
    public float Horizontal  { get { return input.x; } }
    Vector2 JoyPosition = Vector2.zero;

    void Start() {
        Background.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventdata) {
        Background.gameObject.SetActive(true);
        OnDrag(eventdata);
        JoyPosition = eventdata.position;
        Background.position = eventdata.position;
        Handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventdata) {
        Vector2 JoyDirection = eventdata.position - JoyPosition;
        input = (JoyDirection.magnitude > Background.sizeDelta.x / 2f) ? JoyDirection.normalized : JoyDirection / (Background.sizeDelta.x / 2f);
        if (JoyStickDirection == JoyStickDirection.Horizontal) {
            input = new Vector2(input.x, 0f);
        }
        if (JoyStickDirection == JoyStickDirection.Vertical) {
            input = new Vector2(0f, input.y);
        }
        Handle.anchoredPosition = (input * Background.sizeDelta.x / 2f) * HandleLimit;
    }

    public void OnPointerUp(PointerEventData eventdata) {
        Background.gameObject.SetActive(false);
        input = Vector2.zero;
        Handle.anchoredPosition = Vector2.zero;
    }
}
