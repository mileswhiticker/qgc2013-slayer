using UnityEngine;
using System.Collections;

public class Charger : MonoBehaviour {
	
	private Transform charger;
	private Transform target;
	
	public int moveSpeed = 4;
	public int chargeSpeed = 10;
	public int fallSpeed = 4;
	public int damage = 2;
	
	public int chaseRange = 10;
	public int chargeRange = 10;
	
	private float dist;
	
	private float chargeTime;
	public float chargeTimer = 1;
	
	private float coolDownTime;
	public float coolDownTimer = 1;
	
	private int moveState = MOVE_LEFT;
	private const int MOVE_LEFT = 0;
	private const int MOVE_RIGHT = 1;
	
	private int chargerState = 0;
	
	private const int CHARGER_MOVE = 0;
	private const int CHARGER_CHARGE = 1;
	private const int CHARGER_REST = 2;
	private const int CHARGER_STAIRS_ASCEND = 3;
	private const int CHARGER_STAIRS_DESCEND = 4;
	private const int CHARGER_FALLING = 5;
	private const int CHARGER_CHASE = 6;

	private Vector3 chargerLeftExtent;
	private Vector3 chargerRightExtent;
	
	private RaycastHit hit;
	private float detectDist;
	
	//health
	public int maxHealth = 3;
	private int health = 3;
	
	// Use this for initialization
	void Start () 
	{
		charger = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//print(chargerState);
		if (health <= 0)
		{
			charger.gameObject.SetActiveRecursively(false);		
		}
		
		switch(chargerState)
		{
			case (CHARGER_MOVE):
			{
				//check direction and cast ray to find player
				detectDist = charger.collider.bounds.extents.x;
				switch(moveState)
				{
					case (MOVE_LEFT):
					{
						RaycastHit[] hits = Physics.RaycastAll(charger.position,Vector3.left,chaseRange);
						foreach(RaycastHit strike in hits)
						{
							if (strike.collider.tag == "Player")
							{
								print ("hit");
								chargerState = CHARGER_CHASE;
								break;
							}
						}
				
						//detect if obstructed
						if (Physics.Raycast(charger.position,Vector3.left,out hit,detectDist))
						{
							if (hit.collider.tag == "Obstruction")
							{
								moveState = MOVE_RIGHT;
								break;
							}
						}
				
						//DETECT EDGE/////////////////////////////////////////////////////////////
						CheckEdge();
				
						//MOVE THE BITCH
						Vector3 tempPos = charger.position;
						tempPos.x -= moveSpeed * Time.deltaTime;
						charger.position = tempPos;
						break;
					}
					case (MOVE_RIGHT):
					{
						RaycastHit[] hits = Physics.RaycastAll(charger.position,Vector3.right,chaseRange);
						foreach(RaycastHit strike in hits)
						{
							if (strike.collider.tag == "Player")
							{
								chargerState = CHARGER_CHASE;
							}
						}
				
						//detect if obstructed
						if (Physics.Raycast(charger.position,Vector3.right,out hit,detectDist))
						{
							if (hit.collider.tag == "Obstruction")
							{
								moveState = MOVE_LEFT;
								break;
							}
						}
				
						//DETECT EDGE/////////////////////////////////////////////////////////////
						CheckEdge();
				
						//MOVE THE BITCH
						Vector3 tempPos = charger.position;
						tempPos.x += moveSpeed * Time.deltaTime;
						charger.position = tempPos;
						break;
					}
				}
			
			
				CheckGround();
				break;
			}
			
			case (CHARGER_CHARGE):
			{
				if (chargeTime < 0)
				{
					coolDownTime = coolDownTimer;
					chargerState = CHARGER_REST;
					break;
				}
				chargeTime -= Time.deltaTime;
			
				switch(moveState)
				{
					case (MOVE_LEFT):
					{
						//detect if obstructed
						if (Physics.Raycast(charger.position,Vector3.left,out hit,detectDist))
						{
							if (hit.collider.tag == "Obstruction")
							{
								moveState = MOVE_RIGHT;
								break;
							}
						}
				
						
				
						//MOVE THE BITCH
						Vector3 tempPos = charger.position;
						tempPos.x -= chargeSpeed * Time.deltaTime;
						charger.position = tempPos;
						break;
					}
					case (MOVE_RIGHT):
					{
						//detect if obstructed
						if (Physics.Raycast(charger.position,Vector3.right,out hit,detectDist))
						{
							if (hit.collider.tag == "Obstruction")
							{
								moveState = MOVE_LEFT;
								break;
							}
						}
				
						
				
						//MOVE THE BITCH
						Vector3 tempPos = charger.position;
						tempPos.x += chargeSpeed * Time.deltaTime;
						charger.position = tempPos;
						break;
					}
				}
				
			
				CheckGround();
				break;
			}
			//////////////////REST/////////////////////
			#region
			case (CHARGER_REST):
			{
				if (coolDownTime < 0)
				{
					chargerState = CHARGER_MOVE;
					break;
				}
				coolDownTime -= Time.deltaTime;
			
				//ANIMATE TIREDNESS
				
				break;
			}
			#endregion
			///////////////////////////////////////////
			case (CHARGER_STAIRS_ASCEND):
			{
				
				break;
			}
			case (CHARGER_STAIRS_DESCEND):
			{
				
				break;
			}
			/////////////////FALLING///////////////////
			#region
			case (CHARGER_FALLING):
			{
				//CATCH
				chargerLeftExtent = new Vector3((charger.position.x - charger.collider.bounds.extents.x),charger.position.y,charger.position.z);
				chargerRightExtent = new Vector3((charger.position.x + charger.collider.bounds.extents.x),charger.position.y,charger.position.z);
				if ( (Physics.Raycast(chargerLeftExtent,Vector3.down,out hit,charger.collider.bounds.extents.y)) || (Physics.Raycast(chargerRightExtent,Vector3.down,out hit,charger.collider.bounds.extents.y)) )
				{
					if (hit.collider.tag == "Obstruction")
					{
						Vector3 tempPos = charger.position;
						tempPos.y = hit.point.y + charger.collider.bounds.extents.y;
						charger.position = tempPos;
						chargerState = CHARGER_MOVE;
						break;
					}
				}
				
				CheckGround();
			
				//MOVE THE BITCH
				Vector3 curPos = charger.position;
				curPos.y -= fallSpeed * Time.deltaTime;
				charger.position = curPos;
				break;
			}
			#endregion
			///////////////////////////////////////////
			case (CHARGER_CHASE):
			{
				
				switch(moveState)
				{
					case (MOVE_LEFT):
					{
						RaycastHit[] hits = Physics.RaycastAll(charger.position,Vector3.left,chargeRange);
						foreach(RaycastHit strike in hits)
						{
							if (strike.collider.tag == "Player")
							{
								chargeTime = chargeTimer;
								chargerState = CHARGER_CHARGE;
								break;
							}
						}
						//detect if obstructed
						if (Physics.Raycast(charger.position,Vector3.left,out hit,detectDist))
						{
							if (hit.collider.tag == "Obstruction")
							{
								moveState = MOVE_RIGHT;
								break;
							}
						}
						
						//DETECT EDGE/////////////////////////////////////////////////////////////
						CheckEdge();
				
						//MOVE THE BITCH
						Vector3 tempPos = charger.position;
						tempPos.x -= moveSpeed * Time.deltaTime;
						charger.position = tempPos;
						break;
					}
					case (MOVE_RIGHT):
					{
						RaycastHit[] hits = Physics.RaycastAll(charger.position,Vector3.right,chargeRange);
						foreach(RaycastHit strike in hits)
						{
							if (strike.collider.tag == "Player")
							{
								chargeTime = chargeTimer;
								chargerState = CHARGER_CHARGE;
							}
						}
				
						//detect if obstructed
						if (Physics.Raycast(charger.position,Vector3.right,out hit,detectDist))
						{
							if (hit.collider.tag == "Obstruction")
							{
								moveState = MOVE_LEFT;
								break;
							}
						}
				
						//DETECT EDGE/////////////////////////////////////////////////////////////
						CheckEdge();
				
						//MOVE THE BITCH
						Vector3 tempPos = charger.position;
						tempPos.x += moveSpeed * Time.deltaTime;
						charger.position = tempPos;
						break;
					}
				}
			
			
				CheckGround();
				break;
			}
		}
		
	}
	
	void CheckGround()
	{
		//check for ground
		chargerLeftExtent = new Vector3((charger.position.x - charger.collider.bounds.extents.x),charger.position.y,charger.position.z);
		chargerRightExtent = new Vector3((charger.position.x + charger.collider.bounds.extents.x),charger.position.y,charger.position.z);

		Vector3 castPos = charger.position;
		castPos.x -= charger.collider.bounds.extents.x * 1.5f;
		RaycastHit[] hits = Physics.RaycastAll(castPos, Vector3.down, charger.collider.bounds.extents.y);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				//chargerState = CHARGER_MOVE;
				return;
			}
		}
		
		//if there isn't, check for geometry below the right corner
		castPos.x += 2 * charger.collider.bounds.extents.x * 1.5f;
		hits = Physics.RaycastAll(castPos, Vector3.down, charger.collider.bounds.extents.y);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				//chargerState = CHARGER_MOVE;
				return;
			}
		}
		
		chargerState = CHARGER_FALLING;
	}
	
	void CheckEdge()
	{
		//check for ground
		chargerLeftExtent = new Vector3((charger.position.x - charger.collider.bounds.extents.x),charger.position.y,charger.position.z);
		chargerRightExtent = new Vector3((charger.position.x + charger.collider.bounds.extents.x),charger.position.y,charger.position.z);

		RaycastHit[] hits = Physics.RaycastAll(chargerLeftExtent, Vector3.down, charger.collider.bounds.extents.y);
		bool geometryBelow = false;
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				geometryBelow = true;
				break;
			}
		}
		if(!geometryBelow)
		{
			moveState = MOVE_RIGHT;
		}
		
		geometryBelow = false;
		hits = Physics.RaycastAll(chargerRightExtent, Vector3.down, charger.collider.bounds.extents.y);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				geometryBelow = true;
				break;
			}
		}
		if(!geometryBelow)
		{
			moveState = MOVE_LEFT;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
			
	}
	
	
	void OnCollisionEnter(Collision col)
	{
//		if (col.gameObject.tag == "Player")
//		{
//			//inflict damage
//			Player playerScript = (Player)col.gameObject.GetComponent("Player");
//			playerScript.TakeDamage(damage);	
//		}
	}
	
	public void TakeDamage(int damage)
	{
		health -= damage;	
	}
}
