using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;

public class Commands : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnitMovement[] units = FindObjectsOfType<UnitMovement>();
        TrenchSlot[] trenchSlots = FindObjectsOfType<TrenchSlot>();
        foreach (UnitMovement x in units)
        {
            if (x.gameObject.GetComponent<Unit>() != null)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    x.commandQueue.Add("Move");
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    x.commandQueue.Add("Stay");
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    x.commandQueue.Add("Retreat");
                }
            }
        }
        
    }
}
