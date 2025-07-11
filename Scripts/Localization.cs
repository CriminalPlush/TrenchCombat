using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using YG;

public class Localization : MonoBehaviour
{
    [SerializeField] private string russian;
    [SerializeField] private string english;
    [SerializeField] private TMP_Text text;
    void Start()
    {
        UpdateLanguage();
    }
    public void UpdateLanguage()
    {
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
        }
        if (YG2.isSDKEnabled)
        {
            switch (YG2.lang)
            {
                case "ru":
                    text.text = russian;
                    break;
                case "en":
                    text.text = english;
                    break;
                default:
                    text.text = english;
                    break;
            }
        }
        else
        {
            text.text = russian;
        }
    }
}
