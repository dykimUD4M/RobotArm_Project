using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBall : MonoBehaviour
{
    private List<Transform> balls = new List<Transform>();

    private void Awake()
    {
        for(int i = 0;i<transform.childCount;i++)
        {
            balls.Add(transform.GetChild(i));
        }
    }
    public void Change()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].gameObject.SetActive(false);
        }
    }
}
