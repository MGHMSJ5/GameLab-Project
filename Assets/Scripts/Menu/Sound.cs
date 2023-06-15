using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    private static float sliderValue = 1;
    [SerializeField] Slider soundSlider;

    public List<AudioSource> AllAudio = new List<AudioSource>(); //all the audio

    public void Start()
    {
        soundSlider.value = sliderValue;
    }

    public void Update()
    {
        sliderValue = soundSlider.value;
        for (int i = 0; i < AllAudio.Count; i++)
        {
            AllAudio[i].volume = sliderValue; //change the audio value based on the slider
        }
    }

    public void SoundOn()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
    }

    public void SoundOff()
    {
        AudioListener.volume = 0;
    }

    public void ChangeSound()
    {
        //AudioListener.volume = sliderValue;
    }
}
