using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBoosts : MonoBehaviour
{
    [SerializeField] private GameObject trenchBoostIcon;
    [SerializeField] private GameObject officerBoostIcon;
    [SerializeField] private Unit unit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        officerBoostIcon.SetActive(unit.isOfficerBoosted);
        trenchBoostIcon.SetActive(unit.isTrenchBoosted);
    }
}
