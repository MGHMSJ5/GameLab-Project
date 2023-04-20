using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //to be able to change scenes in Unity

public class MenuCode : MonoBehaviour
{

    public void PlayGame() //a function that'll be called whenever the 'Play'button is pressed
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //go to the next scene that is in the queue (In Build Settings).
    }

    public void ExitGame() // a function that'll be calld whenever the 'Exit' button is pressed
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //go to the previous scene that is in the queue (In Build Settings).
        Time.timeScale = 1f;
    }
    public void QuitGame() //a funciton that'll be called when the 'Quit' button is pressed
    {
        Debug.Log("QuitGame"); //to see if it works
        Application.Quit(); //Quit the game
    }
}
