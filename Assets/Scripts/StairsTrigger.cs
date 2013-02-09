using UnityEngine;
using System.Collections;

public class StairsTrigger : MonoBehaviour
{
	//set this to the other trigger in the stairs
	public GameObject SETME_stairsTrigger;

	// Use this for initialization
	void Start ()
	{
		if(SETME_stairsTrigger)
		{
			StairsTrigger otherStairScript = (StairsTrigger)SETME_stairsTrigger.GetComponent("StairsTrigger");
			if(!otherStairScript)
			{
				SETME_stairsTrigger.AddComponent("StairsTrigger");
				otherStairScript = (StairsTrigger)SETME_stairsTrigger.GetComponent("StairsTrigger");
			}
			if(!otherStairScript.SETME_stairsTrigger)
			{
				otherStairScript.SETME_stairsTrigger = this.gameObject;
			}
		}
	}
}
