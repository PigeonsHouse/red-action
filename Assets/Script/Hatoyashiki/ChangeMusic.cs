using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public AudioClip normalClip;
    public AudioClip bossClip;
    public AudioClip clearClip;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(int musicNumber){
        switch(musicNumber){
            case 0:
                audio.Stop();
                break;
            case 1:
                audio.loop = true;
                audio.clip = normalClip;
                audio.Play();
                break;
            case 2:
                audio.loop = true;
                audio.clip = bossClip;
                audio.Play();
                break;
            case 3:
                audio.loop = false;
                audio.clip = clearClip;
                audio.Play();
                break;
            default:
                break;
        }
    }

}
