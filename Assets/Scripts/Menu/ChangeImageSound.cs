using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //add this to be able to use Image

public class ChangeImageSound : MonoBehaviour
{
	public Image off;
	public Image on;

	public Sprite off2;
	public Sprite on2;

	public static bool SLeftChecking = false;
	public static bool SRightChecking = false;

	public void SoundCheckLeft()
	{
		SRightChecking = false;
		SLeftChecking = true;
	}

	public void SoundCheckRight()
	{
		SRightChecking = true;
		SLeftChecking = false;
	}


	private void Update()
	{
		if (SLeftChecking)
		{
			off.sprite = on2;
			on.sprite = off2;
		}

		if (SRightChecking)
		{
            on.sprite = on2;
			off.sprite = off2;
		}
	}

}
