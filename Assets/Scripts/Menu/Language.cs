using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //add this to be able to use Image

public class Language : MonoBehaviour
{
	public Image off;
	public Image on;

	public Sprite off2;
	public Sprite on2;

	public static bool LLeftChecking = false;
	public static bool LRightChecking = false;

	public void LanguageCheckLeft()
	{
		LRightChecking = false;
		LLeftChecking = true;
	}

	public void LanguageCheckRight()
	{
		LRightChecking = true;
		LLeftChecking = false;
	}


	private void Update()
	{
		if (LLeftChecking)
		{
			off.sprite = on2;
			on.sprite = off2;
		}

		if (LRightChecking)
		{
			on.sprite = on2;
			off.sprite = off2;
		}
	}

}
