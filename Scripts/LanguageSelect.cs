using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelect : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.GetString("language", "") == "")
        {
            gameObject.SetActive(true);
        }
    }
}
