using UnityEngine;
using System.Collections;

public class BoobyTrap : MonoBehaviour {
	
	private Transform turret;
	private Transform player;
	private Transform barrel;
	public GameObject bullet;
	
	public float upAng;
	public float downAng;
	private float rotStart;
	public int rotSpeed;
	public int detRad;
	
	private int rotState;
	private const int ROTATEDEAD = 0; 
	private const int ROTATE = 1;
	private const int ROTATERESET = 2;
	
	
	// Use this for initialization
	void Start () 
	{
		rotState = 0;
		turret = this.transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		barrel = turret.GetChild(0).transform;
		rotStart = turret.eulerAngles.z;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		print (turret.eulerAngles.z);
		if (player != null)
		{
			switch(rotState)
			{
				case (ROTATEDEAD):	
				{
					float dist = Vector3.Distance(player.position,turret.position);
					Vector3 diff = player.position - turret.position;
					if (dist <= detRad)
					{
						RaycastHit hit;
						if (Physics.Raycast(turret.position,diff,out hit,detRad))
						{
							if (hit.collider.tag != "level")
							{
								rotState = ROTATE;
								
							}
						}
					}
						
					break;
				}
				case (ROTATE):
				{
					if (turret.eulerAngles.z < rotStart)
					{
						RotateUp();
					}
					else if (turret.eulerAngles.z > rotStart)
					{
						RotateDown();
					}
					else
					{
						RotateUp();
					}
					break;
				}
				case (ROTATERESET):
				{
					if (turret.eulerAngles.z < rotStart)
					{
						RotateUp();
					}
					else if (turret.eulerAngles.z > rotStart)
					{
						RotateDown();
					}
					break;
				}
			}
		}
			
	}
	
	
	void Shoot()
	{
		//spawn bullet on child
		Instantiate(bullet,barrel.position,turret.rotation);	
	}
	
	void RotateUp()
	{
		Vector3 tempRot = turret.eulerAngles;
		tempRot.z -= rotSpeed * Time.deltaTime;
		turret.eulerAngles = tempRot;
			
	}
	
	void RotateDown()
	{
		Vector3 tempRot = turret.eulerAngles;
		tempRot.z += rotSpeed * Time.deltaTime;
		turret.eulerAngles = tempRot;	
	}
}
