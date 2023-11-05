using System.Collections;
using System.Collections.Generic;
using NongTrai;
using UnityEngine;
using UnityEngine.UI;

public class ToastManager : Singleton<ToastManager>
{
    [SerializeField] private GameObject toast;
    [SerializeField] private Text _title, _content;

    public void Show(string content, string title = "Notify")
    {
        toast.SetActive(true);
        _title.text = title;
        _content.text = content;
    }

    public void Close()
    {
        toast.SetActive(false);
    }
}