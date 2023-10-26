using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using NongTrai;
using UnityEngine;
using UnityEngine.UI;

public class NameTagManager : Singleton<NameTagManager>
{
    [SerializeField] private GameObject _open;
    [SerializeField] private InputField _inputField;

    private void Start()
    {
        ShowName();
    }

    [Button]
    public void OpenPopup()
    {
        _open.SetActive(true);
        ShowName();
    }

    private void ShowName()
    {
        _inputField.text = PlayerPrefs.GetString("NameTag", "NameTag");
    }

    public void SetName()
    {
        PlayerPrefs.SetString("NameTag", _inputField.text);
        _open.SetActive(false);
    }
}