using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutCommand : MonoBehaviour
{
    [SerializeField]
    private GameObject playerTrench;
    [SerializeField]
    private GameObject enemyTrench;

    void Start()
    {

    }

    // Update is called once per frame
    public void OnClick()
    {
        foreach (Transform x in playerTrench.transform)
        {
            if (x.gameObject.GetComponent<TrenchSlot>() != null && x.gameObject.GetComponent<TrenchSlot>().unit != null)
            {
                TrenchSlot slot = x.gameObject.GetComponent<TrenchSlot>();
                if (slot.unit.GetComponent<UnitMovement>() != null && slot.unit.GetComponent<Unit>().isEnemy == false)
                {
                    UnitMovement UM = slot.unit.GetComponent<UnitMovement>();
                    UM.Move();
                    if (UM.trenchIndex == playerTrench.GetComponent<Trench>().index) // prevents increasing index more than once (I guess)
                    {
                        x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>().trenchIndex++;
                    }
                }
            }
        }
    }
    public void GetOutEnemy()
    {
        foreach (Transform x in enemyTrench.transform)
        {
            if (x.gameObject.GetComponent<TrenchSlot>() != null && x.gameObject.GetComponent<TrenchSlot>().unit != null)
            {
                TrenchSlot slot = x.gameObject.GetComponent<TrenchSlot>();
                if (slot.unit.GetComponent<UnitMovement>() != null && slot.unit.GetComponent<Unit>().isEnemy == true)
                {
                    UnitMovement UM = slot.unit.GetComponent<UnitMovement>();
                    UM.Move();
                    if (UM.trenchIndex == enemyTrench.GetComponent<Trench>().index) // prevents increasing index more than once (I guess)
                    {
                        x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>().trenchIndex--;
                    }
                }
            }
        }
    }
}
