using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUnitData : MonoBehaviour
{
    [SerializeField] UnitInfo unitInfo;
    [SerializeField] RawImage image;
    [SerializeField] TMP_Text description;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnClick()
    {
        image.texture = unitInfo.picture;
        description.text = unitInfo.description;
        FindObjectOfType<BuyUpgrade>().unitInfo = unitInfo;
    }
}
