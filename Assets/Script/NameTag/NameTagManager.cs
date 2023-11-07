using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using NongTrai;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NameTagManager : Singleton<NameTagManager>
{
    [FormerlySerializedAs("_open")] [SerializeField] private GameObject open;
    [FormerlySerializedAs("_inputField")] [SerializeField] private InputField inputField;

    private void Start()
    {
        ShowName();
    }

    [Button]
    public void OpenPopup()
    {
        open.SetActive(true);
        ShowName();
    }

    public void SetName()
    {
        PlayerPrefs.SetString("NameTag", inputField.text);
        open.SetActive(false);
    }
    
    private void ShowName()
    {
        inputField.text = PlayerPrefs.GetString("NameTag", "NameTag");
    }
}