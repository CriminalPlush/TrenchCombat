using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoldDisplay : MonoBehaviour
{
    public TMP_Text text;
    private PlayerResources res;
    
    void Start()
    {
        res = FindObjectOfType<PlayerResources>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Convert.ToString(res.gold);
    }
}
