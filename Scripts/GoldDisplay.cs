using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoldDisplay : MonoBehaviour
{
    public TMP_Text text;
    private Resources res;
    
    void Start()
    {
        res = FindObjectOfType<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Convert.ToString(res.gold);
    }
}
