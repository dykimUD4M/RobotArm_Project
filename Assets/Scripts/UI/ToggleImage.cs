using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    [SerializeField]
    private Sprite _selectImage;
    [SerializeField]
    private Sprite _enableImage;

    [SerializeField]
    bool _isImage = true;

    [SerializeField]
    private UnityEvent _onClickEvent;

    private Toggle _toggle;
    private Image _image;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _image = GetComponent<Image>();
    }

    public void ImageSet()
    {
        Debug.Log(_toggle.isOn);
        if(_toggle.isOn)
        {
            if (_isImage)
                _image.sprite = _selectImage;
            _onClickEvent.Invoke();
        }
        else
        {
            if (_isImage)
                _image.sprite = _enableImage;
        }
    }
}
