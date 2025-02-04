using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpelUnitCommand : MonoBehaviour
{
    [SerializeField]
    private GameObject[] expelButtons;
    [SerializeField]
    private Transform trench;
    private List<GameObject> units= new List<GameObject>();
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
        foreach(Transform x in trench)
        {
            if(x.GetComponent<TrenchSlot>() != null)
            {
                if(x.GetComponent<TrenchSlot>().unit != null)
                {
                    units.Add(x.GetComponent<TrenchSlot>().unit);
                }
            }
        }
        for(int i = 0; i < units.Count; i++)
        {
            expelButtons[i].GetComponent<UnitInfoSlot>().unitInfo = units[i].GetComponent<UnitInfo>();
            expelButtons[i].GetComponent<RawImage>().texture = expelButtons[i].GetComponent<UnitInfoSlot>().unitInfo.icon;
        }
    }
}
