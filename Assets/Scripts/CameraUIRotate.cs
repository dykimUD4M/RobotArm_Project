using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraUIRotate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField]
    private Transform _uiCameraPivot;
    //[SerializeField]
    //private Transform _uiCameraYTrm;
    //[SerializeField]
    //private Transform _uiCameraZTrm;
    [SerializeField]
    private Transform _targetTrm;

    //[SerializeField]
    //private Transform _perspCameraPivot;
    [SerializeField]
    private Transform _cameraTrm;
    //[SerializeField]
    //private float _posMoveScale = 0.5f;
    [SerializeField]
    private float _rotateMoveScale = 1f;
    [SerializeField]
    private float _distanceSpeed = 10f;

    private bool _isFocus = false;

    private Vector2 _curPos;

    private float _cameraDistance = 1f;


    private float _curYAngle = 0;
    private float _curZAngle = 0;
    private float _yAngle = 0;
    private float _zAngle = 0;
    private Vector2 _rotateClickPos;
    private Quaternion _clickCameraRot = Quaternion.identity;



    private void Update()
    {
        if (_isFocus == false) return;

        MouserZoomInOut();
        //MouserLeftClick();
        //MouserRightClick();
    }

    int _idx = 0;
    public TextMeshProUGUI _text;
    public void SetRot(int idx)
    {
        _idx += idx;
        if(_idx < 0)
            _idx = 3;
        if(_idx >= 4)
            _idx = 0;
        _uiCameraPivot.DORotate(new Vector3(-49, -_idx * 90, 0), 1);
        _text.SetText(_idx.ToString());
        Debug.Log($"Chanage Camera : {_idx}");
    }
    private void MouserZoomInOut()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            _cameraDistance -= _distanceSpeed * Time.deltaTime;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            _cameraDistance += _distanceSpeed * Time.deltaTime;
        }
        _cameraDistance = Mathf.Clamp(_cameraDistance, 0.1f, 1.5f);
        _cameraTrm.localPosition = new Vector3(0, 0, _cameraDistance);
    }

    private void MouserRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Click");
            _rotateClickPos = _curPos;
            _clickCameraRot = _uiCameraPivot.rotation;

            _yAngle = _curYAngle;
            _zAngle = _curZAngle;
        }
        if (Input.GetMouseButton(1))
        {
            //Debug.Log("Clicked");
            Vector3 moveDir = _curPos - _rotateClickPos;

            _curYAngle = _yAngle + (moveDir.x * _rotateMoveScale);
            _curZAngle = Mathf.Clamp(_zAngle + (moveDir.y * _rotateMoveScale), -60, 60);
            //Debug.Log($"yAngle : {_curYAngle} , zAngle : {_curZAngle}");

            Quaternion quaternion = Quaternion.Euler(_curZAngle, _curYAngle, 0);
            _uiCameraPivot.rotation = quaternion;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isFocus = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isFocus = false;
        _rotateClickPos = Vector2.zero;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_isFocus == false) return;

        _curPos = eventData.position;
    }
}
