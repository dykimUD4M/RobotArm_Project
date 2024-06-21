using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public Button _nextBtn;
    public Button _beforeBtn;

    public Button _frontBtn;
    public Button _backBtn;


}
