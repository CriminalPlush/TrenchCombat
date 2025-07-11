using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private string levelRequired = null;
    [SerializeField] private GameObject block;

    void Start()
    {
        if (YG2.saves.playerData.levelsPassed.Contains(levelRequired) == false && levelRequired != null && levelRequired != "")
        {
            block.SetActive(true);
        }
        else
        {
            block.SetActive(false);
        }
    }
    public void LoadScene()
    {
        Debug.Log("cho");
        SceneManager.LoadScene(levelName);
    }
}
