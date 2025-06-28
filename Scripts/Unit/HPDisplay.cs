using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    [SerializeField]
    private Unit unit;
    [SerializeField]
    private Slider slider;
    private float maxHp;
    void Start()
    {
        maxHp = unit.HP;
        slider.maxValue = unit.HP;
        slider.value = unit.HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (unit.HP == maxHp)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
            slider.value = unit.HP;
            slider.gameObject.transform.LookAt(Camera.main.transform);
        }
    }
}
