using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using Script.GameUI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class NameTag : BaseBuild
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Canvas _canvas;

    private void OnEnable()
    {
        SetName();
    }

    protected override void Start()
    {
        base.Start();
        _canvas.sortingOrder = (int) (order + 1);
    }

    [Button]
    protected override void OpenPopup()
    {
        NameTagManager.Instance.OpenPopup();
    }

    public void SetName()
    {
        _nameText.text = PlayerPrefs.GetString("NameTag", "NameTag");
    }
}