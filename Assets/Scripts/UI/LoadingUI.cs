using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] Image _circle1;
    [SerializeField] Image _circle2;

    [SerializeField] TextMeshProUGUI _complateValueText;

    [SerializeField] float _speed;
    float value = 0;
    public bool isComplate = false;
    float complateValue = 0;
    private void Update()
    {
        value += Time.deltaTime * _speed;
        _circle1.transform.rotation = Quaternion.Euler(0, 0, -value);
        _circle2.transform.rotation = Quaternion.Euler(0, 0, value);

        if(isComplate)
        {
            complateValue += Time.deltaTime;
            complateValue = Mathf.Clamp(complateValue, 0, 1);

            _complateValueText.SetText($"{((int)(100f * complateValue))}%");

        }
    }


}
