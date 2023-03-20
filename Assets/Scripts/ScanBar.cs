using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanBar : MonoBehaviour
{
    public Slider slider; //reference to the slider

    public void SetMaxScan(int scan) //mehtod to set the slider to max (will be called from other script when game starts)
    {
        slider.maxValue = scan; //set the max value to 'energy'
        slider.value = scan; //set the current value to 'energy'
    }
    public void SetScan(int scan) //method to set the slider (will be called when losing energy)
    {
        slider.value = scan; //set the slider to the new value
    }
}
