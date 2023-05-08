using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource _source;
    [SerializeField] Slider volumeSlider;
    private void Awake()
    {
        volumeSlider.value = 0.5f;
        instance = this;
        _source = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //detect if there is already an instance of SoundManager -> sound will never cut off
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
            
    }
    public void PlaySound(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
    
    public void SetVolume()
    {
        _source.volume = volumeSlider.value;
    }
    private void Update()
    {
        SetVolume();
    }
}
