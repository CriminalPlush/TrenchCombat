using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrenchWalls : MonoBehaviour
{
    [SerializeField] private Trench trench;
    [SerializeField] private GameObject[] walls;
    void FixedUpdate()
    {
        if (trench.HasAnyFreeSlot())
        {
            foreach (var wall in walls)
            {
                wall.SetActive(true);
            }
        }
        else
        {
            foreach (var wall in walls)
            {
                wall.SetActive(false);
            }
        }
    }
}
