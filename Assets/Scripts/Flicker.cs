using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {
	
	private int flickerState;
	
	
	
	// Use this for initialization
	void Start () {
		flickerState = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FlickerWorld()
	{
		GameObject[] fake = GameObject.FindGameObjectsWithTag("Fantasy");
		GameObject[] real = GameObject.FindGameObjectsWithTag("Life");
		switch(flickerState)
		{
			case 0:
				
				foreach(GameObject life in real)
				{
					life.SetActiveRecursively(false);
					
				}
				foreach(GameObject ob in fake)
				{
					ob.SetActiveRecursively(true);
				}
			break;
			case 1:
				
				foreach(GameObject fan in fake)
				{
					fan.SetActiveRecursively(false);

				}
				foreach(GameObject obj in real)
				{
					obj.SetActiveRecursively(true);
				}
			break;
		}
		
		
				
				
		
	}
}
