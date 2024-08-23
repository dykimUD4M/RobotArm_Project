using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatPanelUI : MonoBehaviour
{
    [SerializeField]
    private Transform _content;

    private GameObject _textTmep;
    private void Start()
    {
        _textTmep = _content.GetChild(0).gameObject;

        GameManager.Instance.PoolManager_.CreatePool(_textTmep, 30);
    }

    public void AddText(string text)
    {

        if (_content.childCount >= 30)
            GameManager.Instance.PoolManager_.Push(_content.GetChild(0).GetComponent<Poolable>());

        Poolable go = GameManager.Instance.PoolManager_.Pop(_textTmep, _content);

        go.gameObject.GetComponent<TextMeshProUGUI>().SetText(text);
    }
}
