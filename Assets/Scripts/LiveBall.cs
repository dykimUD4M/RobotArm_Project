using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBall : Poolable
{
    public float _liveTime;


    private float _time;
    private void Update()
    {
        if (isUsing == false) return;

        _time += Time.deltaTime;

        if(_time >= _liveTime )
        {
            GameManager.Instance.PoolManager_.Push(this);
        }
    }
}
