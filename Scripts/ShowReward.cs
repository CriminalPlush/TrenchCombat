using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowReward : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Start()
    {
        int reward = 0;
        if (SaveSystem.Load().levelsPassed.Contains(SceneManager.GetActiveScene().name))
        {
            reward = FindObjectOfType<LevelData>().reward;
        }
        text.text = $"+{reward}<sprite=0>";  
    }
}
