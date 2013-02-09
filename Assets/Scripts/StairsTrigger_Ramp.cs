using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StairsTrigger_Ramp : StairsTrigger
{
	//if you only set one, this ramp will try and link to it's pair
	public GameObject SETME_otherStairsTrigger;
	
	//IGNOREME_stairDir is automatically calculated
	public float IGNOREME_stairsDir = 0;

	// Use this for initialization
	void Start()
	{
		if(SETME_stairsTrigger && !SETME_otherStairsTrigger)
		{
			StairsTrigger otherStairScript = (StairsTrigger)SETME_stairsTrigger.GetComponent("StairsTrigger");
			if(otherStairScript)
			{
				SETME_otherStairsTrigger = otherStairScript.SETME_stairsTrigger;
			}
		}
		else if(SETME_otherStairsTrigger && !SETME_stairsTrigger)
		{
			StairsTrigger otherStairScript = (StairsTrigger)SETME_otherStairsTrigger.GetComponent("StairsTrigger");
			if(otherStairScript)
			{
				SETME_stairsTrigger = otherStairScript.SETME_stairsTrigger;
			}
		}
		
		//IGNOREME_stairDir is the horizontal (positive or negative) direction towards SETME_StarStrigger1
		if(SETME_stairsTrigger && SETME_otherStairsTrigger)
		{
			if(SETME_stairsTrigger.transform.position.x < SETME_otherStairsTrigger.transform.position.x)
			{
				IGNOREME_stairsDir = 1;
			}
			else if(SETME_stairsTrigger.transform.position.x > SETME_otherStairsTrigger.transform.position.x)
			{
				IGNOREME_stairsDir = -1;
			}
		}
	}
}
