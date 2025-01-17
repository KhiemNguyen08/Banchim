﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour
{
    public Text titleText;
    public Text contentText;

    public void UpdateDialog(string title ,string content)
    {
        if (titleText)
            titleText.text = title;
        if (contentText)
            contentText.text = content;
    }
   public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}
