using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] string[] names;
    void Awake()
    {
        names = QualitySettings.names;   
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality",2), true);
        dropdown.value = PlayerPrefs.GetInt("Quality",2);
    }

    // Update is called once per frame
    public void OnValueChanged()
    {
        QualitySettings.SetQualityLevel(dropdown.value,true);
        PlayerPrefs.SetInt("Quality",dropdown.value);
    }
}
