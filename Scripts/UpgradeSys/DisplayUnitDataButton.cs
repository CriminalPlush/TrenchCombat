using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUnitDataButton : MonoBehaviour
{
    [SerializeField] UnitInfo unitInfo;

    // Update is called once per frame
    public void OnClick()
    {
        FindObjectOfType<DisplayUnitData>().Display(unitInfo);
    }
}
