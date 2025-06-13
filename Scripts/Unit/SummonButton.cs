using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonButton : MonoBehaviour
{
    [SerializeField]
    public GameObject unit;
    private UnitInfo unitInfo;
    public float cooldown = 1f;
    public float thisCooldown;
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMP_Text priceText;
    private PlayerResources resources;
    void Start()
    {
        unitInfo = unit.GetComponent<UnitInfoSlot>().unitInfo;
        PlayerData data = SaveSystem.Load();
        if (data.FindUnitByName(unitInfo.name).level == 0)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            image.texture = unitInfo.icon;
            cooldown = unitInfo.spawnCooldown;
            slider.maxValue = cooldown;
            slider.value = thisCooldown;
            priceText.text = Convert.ToString(unitInfo.price);
            resources = FindObjectOfType<PlayerResources>();
        }
    }
    void Update()
    {
        slider.value = thisCooldown;
        if (thisCooldown - Time.deltaTime < 0)
        {
            thisCooldown = 0;
        }
        else if (thisCooldown > 0)
        {
            thisCooldown -= Time.deltaTime;
        }
    }
    public void OnClick()
    {
        if (resources.gold >= unitInfo.price && thisCooldown == 0)
        {
            resources.gold -= unitInfo.price;
            Spawn();
        }

    }
    private void Spawn()
    {
        thisCooldown = cooldown;
        Instantiate(unit, GameObject.FindGameObjectsWithTag("Spawner")[0].transform.position, Quaternion.identity);
    }
}
