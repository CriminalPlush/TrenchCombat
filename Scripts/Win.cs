using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Win : MonoBehaviour
{
    [SerializeField]
    private Unit unit;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private AudioMixerGroup SFXMixer;
    [SerializeField]
    private AudioSource AS;
    private bool hasWon = false;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (unit.HP <= 0)
        {
            //Mathf.MoveTowards(Time.timeScale, 0, 1f * Time.deltaTime);
            //winPanel.SetActive(true);
            Victory();
        }
    }
    private void Victory()
    {
        if (!hasWon)
        {
            hasWon = true;
            AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.outputAudioMixerGroup == SFXMixer)
                {
                    audioSource.Stop();
                }
            }
            winPanel.SetActive(true);
            AS.Play();
            Time.timeScale = 0;
        }
    }
}
