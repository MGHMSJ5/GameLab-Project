using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    public ChangeImageFullscreen changeImageFullscreen;
    private void Update()
    {
        if (changeImageFullscreen.FRight & Input.GetKeyDown(KeyCode.F11))
        {
            changeImageFullscreen.FullscreenCheckLeft();
            FulscreenOFF();
        }
        if (changeImageFullscreen.FLeft & Input.GetKeyDown(KeyCode.F11))
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
