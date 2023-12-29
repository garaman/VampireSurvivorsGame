using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_Joystick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] Image _backGround;
    [SerializeField] Image _handler;

    float _joystickRadius;
    Vector2 _touchPosition;
    Vector2 _moveDir;

    void Start()
    {
        _joystickRadius = _backGround.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2 ; 
    }


    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        Vector2 touchDir = eventData.position - _touchPosition;
        float moveDist = Mathf.Min(touchDir.magnitude, _joystickRadius);
        _moveDir = touchDir.normalized;

        Vector2 newPosition = _touchPosition + _moveDir * moveDist;
        _handler.transform.position = newPosition;

        Managers.Game.MoveDir = _moveDir;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick");
        /*
        _backGround.transform.position = eventData.position;
        _handler.transform.position = eventData.position;

        _touchPosition = eventData.position;
        */
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
        _backGround.transform.position = eventData.position;
        _handler.transform.position = eventData.position;

        _touchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("OnPointerUp");
        _handler.transform.position = _touchPosition;
        _moveDir = Vector2.zero;

        Managers.Game.MoveDir = _moveDir;
    }
}
