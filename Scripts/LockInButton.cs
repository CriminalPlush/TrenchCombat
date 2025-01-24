using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockInButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
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
                if(trench.lockedIn == true)
        {
            text.text = "true";
        }
        else
        {
            text.text = "false";
        }
    }
}
