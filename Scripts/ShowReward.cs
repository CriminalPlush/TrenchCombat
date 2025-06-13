using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowReward : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Start()
    {
        text.text = $"+{FindObjectOfType<LevelData>().reward}<sprite=0>";  
    }
}
