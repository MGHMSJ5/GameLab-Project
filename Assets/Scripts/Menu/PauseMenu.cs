using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public bool GameIsPaused = false; //a varialbe that'll be used to check if the game if paused or not
    public GameObject PauseMenuUI; //reference to the Pause Menu panel as a GameObject
    public KeyCode PauseButton; //the key that can be manually changed in the inspector to pause and un-pause the game

    public GameObject energyUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PauseButton)) //if the key is pressed
        {
            if (GameIsPaused) //if the game is paused (GameIsPaused = true)
            {
                Resume(); //reference to method (un-pause the game, deactivate the UI panel, set GameIsPaused to false)
            }
            else //if GameIsPaused is false. 
            {
                Pause(); //reference to method (pause the game, activate the UI panel, set GameIsPaused to true)
            }
        }
    }

    public void Resume() //this is made public to make it able to be used in buttons
    {
        PauseMenuUI.SetActive(false); //deactivate the Pause Menu panel
        Time.timeScale = 1f; //set the time speed to normal
        GameIsPaused = false; //the game is not paused anymore. So, the 'variable GameIsPaused' is false
        energyUI.SetActive(true); //when the menu is closed/resumed, the energy UI will apear.
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true); //activate the Pause Menu panel
        Time.timeScale = 0f; //freeze the game. timeScale is the speed at which time is passing
        GameIsPaused = true; //the game is paused. So, the variable 'GameIsPaused' is true
        energyUI.SetActive(false); //when the game is paused, the energy UI will dissapear
    }
}
