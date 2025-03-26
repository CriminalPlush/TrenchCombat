using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField]
    private Unit unit;
    [SerializeField]
    private GameObject winPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(unit.HP <= 0)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0, 1f * Time.deltaTime);
            winPanel.SetActive(true);
        }
    }
}
