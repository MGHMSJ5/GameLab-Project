using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public void FullscreenON()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void FulscreenOFF()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}
