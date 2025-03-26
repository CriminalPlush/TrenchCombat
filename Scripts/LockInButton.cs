using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockInButton : MonoBehaviour
{
    [SerializeField]
    private RawImage imageComponent;
    [SerializeField]
    private Texture locked;

    [SerializeField]
    private Texture unlocked;

    [SerializeField]
    private Trench trench;
    void Start()
    {
        CheckIfLockedIn();
    }

    // Update is called once per frame
    public void Click()
    {
        trench.lockedIn = !trench.lockedIn;
        CheckIfLockedIn();
    }
    private void CheckIfLockedIn()
    {
        if (trench.lockedIn == true)
        {
            imageComponent.texture = locked;
        }
        else
        {
            imageComponent.texture = unlocked;
        }
    }
}
