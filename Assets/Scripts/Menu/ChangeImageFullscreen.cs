using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //add this to be able to use Image

public class ChangeImageFullscreen : MonoBehaviour
{
    public Image off; //the image of the left box
    public Image on; //image of the right box

	public Sprite off2; //image of an unchecked box
	public Sprite on2; //image of the checked box

    public static bool FLeftChecking = false; //bool for the left box
	public static bool FRightChecking = true; //bool for the right box
											   //static is there so that the variable also works in other scenes

	public bool FLeft; //bool for the left box
	public bool FRight; //bool for the right box

    public void FullscreenCheckLeft() //method for when the left checkbox is checked
	{
		FRightChecking = false; //right box is false
		FLeftChecking = true; //left box is true
	}

	public void FullscreenCheckRight() //method for when the right checkbox is checked
	{
		FRightChecking = true; //right box is true
		FLeftChecking = false; //left box is false
	}

    private void Update()
	{
		if (FLeftChecking) //left box is clicked, the bool is true
		{
			off.sprite = on2; //the left box will be checked
			on.sprite = off2; //the right box will be unchecked
		}

		if (FRightChecking) //the right box is clicked, the bool is true
		{
			on.sprite = on2; //the right box will be checked
			off.sprite = off2; //the left box will be unchecked
		}

		FLeft = FLeftChecking;
		FRight = FRightChecking;
	}

}
