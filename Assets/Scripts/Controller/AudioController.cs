using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioClip clickAC;
    public AudioClip dropAC;
    public AudioClip controllerAC;
    public AudioClip clearRowAC;

    private Controller controller;

    private bool isMute = false;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<Controller>();
    }
    
    public void PlayClickAC()
    {
        PlayAudio(clickAC);
    }

    public void PlayShapeDropAC()
    {
        PlayAudio(dropAC);
    }

    public void PlayControllerAC()
    {
        PlayAudio(controllerAC);
    }

    public void PlayClearRowAC()
    {
        PlayAudio(clearRowAC);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (isMute)
            return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnMuteClicked()
    {
        isMute = !isMute;
        controller.view.SetMuteBtn(isMute);
        if(!isMute)
        {
            PlayClickAC();
        }
    }
}
