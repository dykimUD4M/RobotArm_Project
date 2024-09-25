using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCompressor : MonoBehaviour
{
    [SerializeField, Header("Ball_Pivot")]
    private Transform _pivot;

    [SerializeField]
    private GameObject _equipBall;
    //private Material _ballMat;
    [SerializeField]
    private GameObject _spawnPhysicBall;
    private bool _isAir = false;
    public bool IsAir
    {
        get { return _isAir; }
        set { _isAir = value;
            if(value)
                Debug.Log("Air");
        }
    }

    private bool _isEquip = false;
    public bool IsEquip
    {
        get { return _isEquip; }
        set { _isEquip = value;}
    }

    private void Awake()
    {
        //_ballMat = _equipBall.GetComponent<Material>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (IsAir == false) return;

        //if(other.CompareTag("Ball") && IsEquip == false)
        //{
        //    other.gameObject.SetActive(false);
        //    Equip();
        //}
    }

    public void Equip()
    {
        if (_equipBall == null) return;

        _equipBall.SetActive(true);
        IsEquip = true;
    }

    public void ChangeColor()
    {

    }

    public void SpawnBall()
    {
        GameObject go = Instantiate(_spawnPhysicBall);
        go.transform.position = _pivot.position;


        Unequip();
    }

    public void Unequip()
    {
        if (_equipBall == null) return;

        _equipBall.SetActive(false);
        StartCoroutine(ReEquipTimer(0.3f));
    }

    private IEnumerator ReEquipTimer(float time)
    {
        yield return new WaitForSeconds(time);
        _isEquip = false;
    }
}
