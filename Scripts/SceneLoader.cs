using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public void OnClick()
    {
        LoadScene(sceneName);
    }
    public void LoadScene(string sceneName)
    {
        Debug.Log("pidro");
        SceneManager.LoadScene(sceneName);
    }
}
