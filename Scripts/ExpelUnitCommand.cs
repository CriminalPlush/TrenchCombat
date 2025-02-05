using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpelUnitCommand : MonoBehaviour
{
    [SerializeField]
    private GameObject[] expelButtons;
    [SerializeField]
    private Transform trench;
    private List<GameObject> units = new List<GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RefreshData();
    }
    private void RefreshData()
    {
        units = new List<GameObject>();
        for (int i = 0; i < expelButtons.Length; i++)
        {
            expelButtons[i].SetActive(false);
            expelButtons[i].GetComponent<UnitInfoSlot>().unitInfo = null;
            expelButtons[i].GetComponentInChildren<TMP_Text>().text = "1";
        }
        foreach (Transform x in trench)
        {
            if (x.GetComponent<TrenchSlot>() != null)
            {
                if (x.GetComponent<TrenchSlot>().unit != null && x.GetComponent<TrenchSlot>().unit.tag == "Unit")
                {
                    units.Add(x.GetComponent<TrenchSlot>().unit);
                }
            }
        }
        int buttonCounter = 0;
        for (int i = 0; i < units.Count; i++)
        {
            bool hasCopy = false;
            foreach (GameObject x in expelButtons)
            {
                if (x.GetComponent<UnitInfoSlot>().unitInfo != null && units[i].GetComponent<UnitInfo>().title == x.GetComponent<UnitInfoSlot>().unitInfo.title)
                {
                    x.SetActive(true);
                    x.GetComponentInChildren<TMP_Text>().text = Convert.ToString(Convert.ToInt32(x.GetComponentInChildren<TMP_Text>().text) + 1);
                    hasCopy = true;
                    break;
                }
            }
            if (!hasCopy)
            {
                expelButtons[buttonCounter].SetActive(true);
                expelButtons[buttonCounter].GetComponent<UnitInfoSlot>().unitInfo = units[i].GetComponent<UnitInfo>();
                expelButtons[buttonCounter].GetComponent<RawImage>().enabled = true;
                expelButtons[buttonCounter].GetComponent<RawImage>().texture = expelButtons[buttonCounter].GetComponent<UnitInfoSlot>().unitInfo.icon;
                buttonCounter++;
            }
        }
    }
}
