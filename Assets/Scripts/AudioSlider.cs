using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string volumeParameterName;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void OnEnable() {
        slider = GetComponent<Slider>();
        mixer.GetFloat(volumeParameterName, out float volume);
        slider.value = Mathf.Pow(10, volume/20);
    }

    public void SetVolume() {
        float volume = slider.value;
        mixer.SetFloat(volumeParameterName, Mathf.Log10(volume)*20);
    }

}
