using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatPanelUI : MonoBehaviour
{
    [SerializeField]
    private Transform _content;
    private GameObject _textTemp;

    [SerializeField]
    private int _poolMaxCnt;

    private void Start()
    {
        _textTemp = _content.GetChild(0).gameObject;

        for(int i = 0;i< _poolMaxCnt;i++)
        {
            Instantiate(_textTemp, _content);
        }
    }

    public void AddText(string text)
    {
        GameObject go = _content.GetChild(0).gameObject;
        go.transform.SetParent(transform);
        go.transform.SetParent(_content);

        go.GetComponent<TextMeshProUGUI>().SetText(text);
    }
}
