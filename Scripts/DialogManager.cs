using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public int index = 0;
    [SerializeField]
    private Message[] messages;
    [SerializeField]
    private TMP_Text messageComponent;
    [SerializeField]
    private TMP_Text messengerComponent;
    [SerializeField]
    private RawImage imgComponent;
    [SerializeField]
    private float typeWriteSpeed = 0.05f;
    [SerializeField]
    private AudioSource clickSound;

    void Start()
    {
        StartCoroutine(TypeWrite());
        UpdateInfo();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    public void Next()
    {
        if (index + 1 < messages.Length)
        {
            index++;
            clickSound.Play();
        }
        else
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
        UpdateInfo();
    }
    public void Previous()
    {
        if (index > 0)
        {
            index--;
            clickSound.Play();
        }
        UpdateInfo();
    }
    private void UpdateInfo()
    {
        messageComponent.maxVisibleCharacters = 0;
        messageComponent.text = messages[index].message;
        messengerComponent.text = messages[index].messenger.name;
        imgComponent.texture = messages[index].messenger.img;
    }
    private IEnumerator TypeWrite()
    {
       // Debug.Log("Sgema");
        yield return new WaitForSecondsRealtime(typeWriteSpeed);
//        Debug.Log("Sgema");
        if(messageComponent.maxVisibleCharacters < messages[index].message.Length) messageComponent.maxVisibleCharacters++;
        else if(messages[index].isInterrupted) Next();
        StartCoroutine(TypeWrite());
    }
}
