using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AirRotator : MonoBehaviour
{

    [SerializeField,Header("Option")]
    private Vector3 _size;
    [SerializeField]
    private Quaternion _rotation;
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private bool _isTrigger;

    private Collider _col;
    private bool _isOn;

    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
            OnTrigger();

        if (_isOn)
        {
            if (_isTrigger == false)
                _isOn = false;

            Shuffle(Physics.OverlapBox(transform.position, _size, _rotation, _layerMask));
        }
        
    }

    public void OnTrigger()
    {
        _isOn = true;
    }

    private void Shuffle(Collider[] cols)
    {
        foreach (Collider col in cols)
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 1, Random.Range(-0.3f,0.3f)) * 0.7f);
        }
    }


#if UNITY_EDITOR
    // OnDrawGizmos()는 Scene 창에서 눈으로 확인하기 위함
    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, _size);
    }
#endif
}
