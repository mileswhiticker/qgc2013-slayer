
using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour {
	
	//macro constants
	private const int PLAYERSTATE_DEFAULT = 0;
	private const int PLAYERSTATE_JUMPING = 1;
	private const int PLAYERSTATE_FALLING = 2;
	private const int PLAYERSTATE_ATTACKING = 3;
	private const int PLAYERSTATE_CLIMBING = 4;
	private const int PLAYERSTATE_LEDGECRAWL = 5;
	private const int PLAYERSTATE_ONSTAIRS = 6;
	private const int PLAYERSTATE_FORCEMOVE = 7;
	
	//player
	public float moveSpeed = 5;
	public int playerState = PLAYERSTATE_DEFAULT;
	public int nextPlayerState = PLAYERSTATE_DEFAULT;
	private float curHorizDir;
	private Transform player;
	
	private float forceMoveSpeed;
	private Vector3 forceMovePos;
	
	//attacking
	public float attackDelay = 2;
	public float attackDuration = 1;
	private float tLeftCantAttack = 0;
	private float tLeftAttacking = 0;
	
	//attack types (todo)
	/*public float lungeTime = 1;
	private float tLeftLunging = 0;
	private int attackState = 0;*/

	// Use this for initialization
	void Start()
	{
		player = this.transform;
	}
	
	// Update is called once per frame
	void Update()
	{
		//update attacking
		if(tLeftCantAttack > 0)
		{
			tLeftCantAttack -= Time.deltaTime;
			if(tLeftCantAttack <= 0)		
			{
				//update sprite?
			}
		}
			
		if(tLeftAttacking > 0)
		{
			tLeftAttacking -= Time.deltaTime;
			if(tLeftAttacking > 0)		
			{
				//attack types
				//update sprite?
				/*switch(attackState)
				{
					case 1:
						if (lungeTime < 0)
						{
							//inflict damage
							lungeTime = lungeTimer;
							attackState = 0;
						}
						lungeTime -= Time.deltaTime;
					break;
				}*/
			}
			else
			{
				//update sprite?
			}
		}
		
		//try attacking
		if (Input.GetKeyDown("x"))
		{
			TryAttack();
		}
		
		//try climbing
		if(tLeftNoLadderClimbing <= 0)
		{
			if(playerState != PLAYERSTATE_CLIMBING)
			{
				TryClimb(Input.GetAxis("Vertical"));
			}
		}
		else
		{
			tLeftNoLadderClimbing -= Time.deltaTime;
		}
		
		switch(playerState)
		{
			case(PLAYERSTATE_DEFAULT):
			{
				PlayerStateProcess_Default();
				break;
			}
			case(PLAYERSTATE_FALLING):
			{
				PlayerStateProcess_Fall();				
				break;
			}
			case(PLAYERSTATE_JUMPING):
			{
				PlayerStateProcess_Jump();
				break;
			}
			case(PLAYERSTATE_CLIMBING):
			{
				PlayerStateProcess_Climb();
				break;
			}
			case(PLAYERSTATE_LEDGECRAWL):
			{
				PlayerStateProcess_Ledgecrawl();
				break;
			}
			case(PLAYERSTATE_ONSTAIRS):
			{
				PlayerStateProcess_OnStairs();
				break;
			}
			case(PLAYERSTATE_FORCEMOVE):
			{
				player.position = Vector3.MoveTowards(player.position, forceMovePos, forceMoveSpeed * Time.deltaTime);
				
				if(player.position == forceMovePos)
				{
					playerState = nextPlayerState;
					//print ("arrived at forceMovePos, new state: " + playerState);
					nextPlayerState = PLAYERSTATE_DEFAULT;
				}
				break;
			}
		}
	}
	
	void TryAttack()
	{
		if(tLeftCantAttack <= 0)
		{
			tLeftAttacking = attackDuration;
			tLeftCantAttack = attackDelay;
		}
	}
	
	public void TakeDamage(int damage)
	{
		//
	}
}
