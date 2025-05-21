using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Start()
    {
        PlayerData playerData = SaveSystem.Load();
        UpdateInfo(playerData.money);   
    }
    public void UpdateInfo(int money)
    {
        text.text = money.ToString();
    }
}
