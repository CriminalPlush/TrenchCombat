using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trench : MonoBehaviour
{
    public bool lockedIn = false;
    public bool HasAnyFreeSlot()
    {
        bool result = false;
        foreach (Transform child in transform)
        {
            if(child.GetComponent<TrenchSlot>() != null && child.GetComponent<TrenchSlot>().unit == null)
            {
                result = true;
            }
        }
        return result;
    }
}
