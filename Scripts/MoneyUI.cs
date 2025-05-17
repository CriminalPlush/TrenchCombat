using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public PlayerData playerData;
    void Awake()
    {
        playerData = SaveSystem.Load();
    }
    void Update()
    {
        text.text = $"{playerData.money}";
    }
}
