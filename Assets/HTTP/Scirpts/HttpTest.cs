using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Http.Loader;
using UnityEngine.UI;
using TMPro;

public class HttpTest : MonoBehaviour
{
    public Toggle _inputtype;
    public InputField _inputField;
    public TextMeshProUGUI _text;

    private string _url;

    public void BtnClick()
    {
        _url = _inputField.text;

        if (_inputtype.isOn)
            HttpManager.GetDataByte(_url, WhiteText);
        else
            HttpManager.GetDataString(_url, WhiteText);
    }

    private void WhiteText(byte[] text)
    {
        
        _text.SetText(string.Join("\r\n", text));
    }
    private void WhiteText(string text)
    {
        _text.SetText(text);
    }
}
