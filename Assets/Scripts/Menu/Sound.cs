using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
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
}
