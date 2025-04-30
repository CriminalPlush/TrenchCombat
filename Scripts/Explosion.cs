using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<Unit>() != null)
        {
            col.GetComponent<Unit>().HP -= 1;
            Destroy(this);
        }
    }
}
