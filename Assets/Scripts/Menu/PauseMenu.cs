using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //to be able to change scenes in Unity

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public bool gameIsPaused = false; //a varialbe that'll be used to check if the game if paused or not
    public GameObject pauseMenuUI; //reference to the Pause Menu panel as a GameObject
    public GameObject optionsMenu;
    public GameObject journal3D;
    public GameObject crossHair;
    public KeyCode pauseButton; //the key that can be manually changed in the inspector to pause and un-pause the game
    public bool canPauseGame = true;

    public GameObject journal3DMaker; //the camera and real 3d model journal

    private void Start()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (canPauseGame)
        {
            if (Input.GetKeyDown(pauseButton)) //if the key is pressed
            {
                if (gameIsPaused) //if the game is paused (GameIsPaused = true)
                {
                    Resume(); //reference to method (un-pause the game, deactivate the UI panel, set GameIsPaused to false)
                }
                else //if GameIsPaused is false. 
                {
                    Pause(); //reference to method (pause the game, activate the UI panel, set GameIsPaused to true)
                }
            }
        }
        if (gameIsPaused)
        {
            Time.timeScale = 0;
        }

    }

    public void Resume() //this is made public to make it able to be used in buttons
    {
        pauseMenuUI.SetActive(false); //deactivate the Pause Menu panel
        optionsMenu.SetActive(false); //deactivate the options menu (if the player exited the menu while having the options open
        journal3D.SetActive(false);
        crossHair.SetActive(true);
        Time.timeScale = 1f; //set the time speed to normal
        Cursor.visible = false;
        gameIsPaused = false; //the game is not paused anymore. So, the 'variable GameIsPaused' is false
        journal3DMaker.SetActive(false);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); //activate the Pause Menu panel
        journal3D.SetActive(true);
        crossHair.SetActive(false);
        Time.timeScale = 0f; //freeze the game. timeScale is the speed at which time is passing
        gameIsPaused = true; //the game is paused. So, the variable 'GameIsPaused' is true
        Cursor.visible = true;
        journal3DMaker.SetActive(true);
    }

    public void ExitGame() // a function that'll be calld whenever the 'Exit' button is pressed
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //go to the previous scene that is in the queue (In Build Settings).
        Time.timeScale = 1f;
    }
}
