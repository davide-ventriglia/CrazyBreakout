using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource[] sources;
    private void Awake() {
        sources = GetComponents<AudioSource>();
    }

    public void PlayClip(AudioClip _clip){
        for(int i=0;i<sources.Length;i++){
            if(!sources[i].isPlaying){
                sources[i].clip = _clip;
                sources[i].Play();
                break;
            }
        }
    }
}
