using UnityEngine;
using System.Collections;


public class AImovementBomb : MonoBehaviour {
	public bool moveLeft;
	public bool moveRight;
	public GameObject bomb;
	
	// Use this for initialization
	void Start () 
	{
		moveLeft = true;
		moveRight = false;
		
//		InvokeRepeating ("dropBomb",0,2);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (moveLeft)
		{
			rigidbody.velocity = new Vector3(-10,-0.5F,0);
		}
		
		if (moveRight)
		{
			rigidbody.velocity = new Vector3(10,0.5F,0);

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
	
	void dropBomb()
	{
		Instantiate (bomb, transform.position, transform.rotation);
	}
	
}