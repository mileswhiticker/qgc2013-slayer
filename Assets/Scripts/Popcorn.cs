using UnityEngine;
using System.Collections;

public class Popcorn : MonoBehaviour {
	
	//ints
	private int health;
	public int maxHealth = 3;
	public int damage = 1;
	
	//floats
	public float moveSpeed = 3;
	
	//Transforms
	private Transform popcorn;
	
	public int moveState = 0;
	private const int MOVE_LEFT = 0;
	private const int MOVE_RIGHT = 1;
	
	private Vector3 popLeftExtent;
	private Vector3 popRightExtent;
	
	private RaycastHit hit;
	private float detectDist;
	
	
	// Use this for initialization
	void Start () 
	{
		health = maxHealth;
		popcorn = this.transform;
		moveState = MOVE_LEFT;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)
		{
			popcorn.gameObject.SetActiveRecursively(false);	
		}
		
		detectDist = (float)(popcorn.collider.bounds.extents.x);
		
		switch(moveState)
		{
			case (MOVE_LEFT):
			{
				
				//detect if obstructed
				if (Physics.Raycast(popcorn.position,Vector3.left,out hit,detectDist))
				{
					if (hit.collider.tag == "Obstruction")
					{
						moveState = MOVE_RIGHT;
						break;
					}
				}
			
				//detect edge
				CheckEdge();
			
				Vector3 tempPos = popcorn.position;
				tempPos.x -= moveSpeed * Time.deltaTime;
				popcorn.position = tempPos;
			
				break;
			}
			case (MOVE_RIGHT):
			{
				
				//detect if obstructed
				if (Physics.Raycast(popcorn.position,Vector3.right,out hit,detectDist))
				{
					if (hit.collider.tag == "Obstruction")
					{
						moveState = MOVE_LEFT;
						break;
					}
				}
			
				//detect edge
				CheckEdge();
			
				Vector3 tempPos = popcorn.position;
				tempPos.x += moveSpeed * Time.deltaTime;
				popcorn.position = tempPos;
			
				break;
			}
		}
		
	}
	
	void CheckEdge()
	{
		//check for ground
		popLeftExtent = new Vector3((popcorn.position.x - popcorn.collider.bounds.extents.x),popcorn.position.y,popcorn.position.z);
		popRightExtent = new Vector3((popcorn.position.x + popcorn.collider.bounds.extents.x),popcorn.position.y,popcorn.position.z);
		if ( (!Physics.Raycast(popLeftExtent,Vector3.down,out hit,popcorn.collider.bounds.extents.y * 2))&& (Physics.Raycast(popRightExtent,Vector3.down,out hit,popcorn.collider.bounds.extents.y * 2)) )
		{
			moveState = MOVE_RIGHT;
		}
		else if ( (Physics.Raycast(popLeftExtent,Vector3.down,out hit,popcorn.collider.bounds.extents.y * 2))&& (!Physics.Raycast(popRightExtent,Vector3.down,out hit,popcorn.collider.bounds.extents.y * 2)) )
		{
			moveState = MOVE_LEFT;
		}
	}
	
	public void TakeDamage (int damage)
	{
		health -= damage;	
	}
	
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			//inflict damage
			Player playerScript = (Player)col.gameObject.GetComponent("Player");
			playerScript.TakeDamage(damage);	
		}
	}
}
