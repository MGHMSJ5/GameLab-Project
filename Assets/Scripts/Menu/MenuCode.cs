using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //to be able to change scenes in Unity

public class MenuCode : MonoBehaviour
{
    public Animator blackFade;
    public void PlayGame() //a function that'll be called whenever the 'Play'button is pressed
    {
        blackFade.SetBool("ToNothing", false);
        blackFade.SetBool("ToBlack", true);
        StartCoroutine(waitForBlack());
    }
    public void QuitGame() //a funciton that'll be called when the 'Quit' button is pressed
    {
        Debug.Log("QuitGame"); //to see if it works
        Application.Quit(); //Quit the game
    }

    IEnumerator waitForBlack()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //go to the next scene that is in the queue (In Build Settings).
    }
}
