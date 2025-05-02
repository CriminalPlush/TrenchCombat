using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackButton : MonoBehaviour
{
    public SpecialAttack specialAttack;
    private Button button;
    private bool isCooldownEnded = true;
    [SerializeField] private Slider slider;
    [SerializeField] private float timeLeft;

    void Start()
    {
        button = GetComponent<Button>();
        slider.maxValue = specialAttack.cooldown;
    }
    void Update()
    {
        if (specialAttack.enabled == false && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else if (timeLeft <0)
        {
            timeLeft = 0;
        }
        slider.value = timeLeft;
    }

    // Update is called once per frame
    public void OnButton()
    {
        if (isCooldownEnded)
        {
            StartCoroutine(Activate());
        }
    }
    private IEnumerator Activate()
    {
        isCooldownEnded = false;
        button.interactable = false;
        specialAttack.enabled = true;
        timeLeft = specialAttack.cooldown;
        yield return new WaitForSeconds(specialAttack.cooldown);
        button.interactable = true;
        isCooldownEnded = true;
    }
}
