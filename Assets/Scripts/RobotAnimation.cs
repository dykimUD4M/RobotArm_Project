using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RobotAnimation : MonoBehaviour
{
    public List<float> waitTimes = new List<float>();

    public UnityEvent action;

    float time = 0;
    int idx = 0;


    public bool _isStart = false;

    public TextMeshProUGUI _textMeshProUGUI;
    public void Toggle()
    {
        _isStart = !_isStart;
        if (_isStart)
            _textMeshProUGUI.SetText("Stop");
        else
            _textMeshProUGUI.SetText("Start");
    }
    private void Update()
    {
        if (_isStart == false) return;

        time += Time.deltaTime;
        if(time >= waitTimes[idx])
        {
            action.Invoke();
            time = 0;
            idx++;
            if (idx >= waitTimes.Count)
                idx = waitTimes.Count-1;
        }
    }
}
