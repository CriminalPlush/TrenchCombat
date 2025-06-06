using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutCommand : MonoBehaviour
{
    [SerializeField]
    private GameObject Trench;
    void Start()
    {

    }

    // Update is called once per frame
    public void OnClick()
    {
        foreach (Transform x in Trench.transform)
        {
            if (x.gameObject.GetComponent<TrenchSlot>() != null && x.gameObject.GetComponent<TrenchSlot>().unit != null)
            {
                if (x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>() != null)
                {
                    x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>().Move();
                    if (x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>().trenchIndex == Trench.GetComponent<Trench>().index) // prevents increasing index more than once (I guess)
                    {
                        x.gameObject.GetComponent<TrenchSlot>().unit.GetComponent<UnitMovement>().trenchIndex++;
                    }
                }
            }
        }

    }
}
