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
        GetOut(false);
    }
    public void GetOutEnemy()
    {
        GetOut(true);
    }
    public void GetOut(bool isEnemy)
    {
        foreach (Transform x in isEnemy ? enemyTrench.transform : playerTrench.transform)
        {
            if (x.gameObject.GetComponent<TrenchSlot>() != null && x.gameObject.GetComponent<TrenchSlot>().unit != null)
            {
                TrenchSlot slot = x.gameObject.GetComponent<TrenchSlot>();
                if (slot.unit.GetComponent<UnitMovement>() != null && slot.unit.GetComponent<Unit>().isEnemy == isEnemy ? true : false)
                {
                    UnitMovement UM = slot.unit.GetComponent<UnitMovement>();
                    UM.Move();
                    if (UM.trenchIndex == playerTrench.GetComponent<Trench>().index) // prevents increasing index more than once (I guess)
                    {
                        if (isEnemy)
                        {
                            x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>().trenchIndex--;
                        }
                        else
                        {
                            x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>().trenchIndex++;
                        }
                    }
                }
            }
        }
    }
}
