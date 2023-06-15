using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public ChangeImageFullscreen changeImageFullscreen; //reference to script that is bound to the buttons in Options
    private void Update()
    {
        if (changeImageFullscreen.FRight & Input.GetKeyDown(KeyCode.F11)) //if fullscreen was on
        {
            changeImageFullscreen.FullscreenCheckLeft();
            FulscreenOFF();
        }
        if (changeImageFullscreen.FLeft & Input.GetKeyDown(KeyCode.F11)) //if fullscreen was off
        { 
            changeImageFullscreen.FullscreenCheckRight();
            FullscreenON();
        }
    }
    public void FullscreenON()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void FulscreenOFF()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}
