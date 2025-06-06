using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrenchIndexer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Trench[] trenches = FindObjectsOfType<Trench>().ToList().OrderBy(go => go.gameObject.transform.position.x).ToArray(); // Orders trenches by X position from left to right
        for (int i = 0; i < trenches.Length; i++)
        {
            trenches[i].index = i;
        }
    }
}
