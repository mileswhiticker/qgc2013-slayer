using UnityEngine;
using System.Collections;


public class AIMoveTele : MonoBehaviour {
	public bool moveLeft;
	public bool moveRight;
	
	
	// Use this for initialization
	void Start () 
	{
		moveLeft = true;
		moveRight = false;
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (moveLeft)
		{
			rigidbody.velocity = new Vector3(-10,0,0);
		}
		
		if (moveRight)
		{
			rigidbody.velocity = new Vector3(10,0,0);
		}
		
	}
	
	void OnCollisionEnter (Collision collision)
	{
		if (collision.rigidbody.tag == "WallLeft")
		moveRight = true;
		moveLeft = false;
		
		if (collision.rigidbody.tag == "WallRight")
		moveRight = false;
		moveLeft = true;
		
	}
	
	
	
	
}
