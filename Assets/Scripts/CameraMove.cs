using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [Serializable]
    public class Info
    {
        public Vector3 pos;
        public Vector3 rot;
    }

    private Camera _camera;

    [SerializeField]
    private List<Info> _cameraPosnRot = new List<Info>();


    int _idx = 0;
    public void CameraMoving(int idx)
    {
        _idx += idx;

        if(_idx <= 0)
            _idx = 0;
        if(_idx >= _cameraPosnRot.Count)
            _idx = _cameraPosnRot.Count-1;

        transform.position = _cameraPosnRot[_idx].pos;
        transform.rotation = Quaternion.Euler(_cameraPosnRot[_idx].rot);
    }
}
