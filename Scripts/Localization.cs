using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        switch (PlayerPrefs.GetString("language", "EN"))
            {
                case "EN":
                    text.text = english;
                    break;
                case "RU":
                    text.text = russian;
                    break;
                default:
                    text.text = english;
                    break;
            }
    }
}
