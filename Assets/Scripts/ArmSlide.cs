using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSlide : MonoBehaviour
{
    private Vector3 _returnPos;
    private bool _ischeck;


    public GameObject _equipBall;
    public Transform pivot;
    public Transform axis_1;
    public Transform axis_2;
    public Transform axis_3;
    public Transform axis_4;

    private void Start()
    {
        _returnPos = _equipBall.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_ischeck == false) return;

        if (other.CompareTag("Ball") && _equipBall == null)
        {
            _equipBall = other.gameObject;
            _equipBall.GetComponent<Rigidbody>().useGravity = false;
            _equipBall.transform.SetParent(this.transform);
            Debug.Log("BallEquip");
        }
    }

    public void Equip()
    {
        
        _equipBall?.SetActive(true);
    }

    public void UnEquip()
    {
        _equipBall?.SetActive(false);
        _equipBall.transform.position = _returnPos;
    }

    Sequence seq;
    public void CheckAnimation()
    {
        seq = DOTween.Sequence();
        seq.Append(axis_2.DOLocalMoveZ(0.001231507f, .6f));
        seq.Append(axis_3.DOLocalRotate(new Vector3(0, 0,8),1f));
        seq.Join(axis_4.DOLocalRotate(new Vector3(0, 0, -8), 1f));
        seq.AppendCallback(() => { _equipBall.transform.SetParent(pivot); });
        seq.Append(axis_2.DOLocalMoveZ(0.001431507f, 0.6f));
        seq.Append(axis_1.DOLocalRotate(new Vector3(-89.98f, 180, 0), 1.6f));
        seq.Append(axis_2.DOLocalMoveZ(0.001231507f, .6f));
        seq.Append(axis_3.DOLocalRotate(new Vector3(0, 0, 0), 1f));
        seq.Join(axis_4.DOLocalRotate(new Vector3(0, 0, 0), 1f));
        seq.AppendCallback(() => { _equipBall.transform.SetParent(null); });
        seq.Append(axis_2.DOLocalMoveZ(0.001431507f, .6f));
    }

    public void ReturnAnimation()
    {
        seq = DOTween.Sequence();
        seq.Append(axis_1.DOLocalRotate(new Vector3(-89.98f, -0, 0), 1.6f));

    }
}
