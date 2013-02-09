using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{	
	if (GUI.Button(new Rect(50,100,100,100), "Restart Level"))
	
			Application.LoadLevel ("harley_test");
	}
	
	
	
}
