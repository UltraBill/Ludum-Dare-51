using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer m_audioMixer;

    public void SetMusicVolume(float vol)
    {
        m_audioMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20f);
    }
    public void SetEffectVolume(float vol)
    {
        m_audioMixer.SetFloat("EffectVolume", Mathf.Log10(vol) * 20f);
    }
}
