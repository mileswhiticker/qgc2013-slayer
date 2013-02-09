using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	//ladders
	public float ladderMoveSpeed = 4;
	private float tLeftNoLadderClimbing = 0;
	public Transform curLadder;
	
	void PlayerStateProcess_Climb()
	{
		//if we're no longer on a ladder, drop off it
		if(!curLadder)
		{
			//print("missing ladder, switching to default state");
			playerState = PLAYERSTATE_DEFAULT;
			return;
		}
		
		float ladderMoveDir = Input.GetAxis("Vertical");
		Vector3 newPos = player.position;
		newPos.y += ladderMoveDir * ladderMoveSpeed * Time.deltaTime;
	
		if (ladderMoveDir != 0)
		{
			//reset the ladder, see if we can find it
			if(ladderMoveDir > 0)
			{
				//check if there is more ladder above us
				if( (player.position.y - player.collider.bounds.extents.y) > (curLadder.transform.position.y + curLadder.collider.bounds.extents.y))
				{
					//print ("climb up from a ladder");
					newPos.y = curLadder.transform.position.y + curLadder.collider.bounds.extents.y + player.collider.bounds.extents.y;
					/*forceMoveSpeed = ladderMoveSpeed;
					forceMovePos = new Vector3(player.position.x, curLadder.transform.position.y + curLadder.collider.bounds.extents.y + player.collider.bounds.extents.y, player.position.z);
					playerState = PLAYERSTATE_FORCEMOVE;
					nextPlayerState = PLAYERSTATE_DEFAULT;*/
				}
			}
			else if(ladderMoveDir < 0)
			{
				//if there is, check left corner for any blocking geometry
				bool blocked = false;
				Vector3 edgeOffset = new Vector3(-player.collider.bounds.extents.x, -player.collider.bounds.extents.y, 0);
				RaycastHit[] hits = Physics.RaycastAll(player.position + edgeOffset, Vector3.down, 0.1f);
				for(int i=0; i<hits.Length; i++)
				{
					if(hits[i].collider.tag == "Obstruction")
					{
						//print ("left corner geometry found blocking ladder");
						newPos.y = hits[i].transform.position.y + hits[i].collider.bounds.extents.y + player.collider.bounds.extents.y;
						playerState = PLAYERSTATE_DEFAULT;
						blocked = true;
						break;
					}
				}
				
				if(!blocked)
				{
					//if it was clear, check right corner
					edgeOffset = new Vector3(player.collider.bounds.extents.x, -player.collider.bounds.extents.y, 0);
					hits = Physics.RaycastAll(player.position + edgeOffset, Vector3.down, 0.1f);
					for(int i=0; i<hits.Length; i++)
					{
						if(hits[i].collider.tag == "Obstruction")
						{
							//print ("right corner geometry found blocking ladder");
							newPos.y = hits[i].transform.position.y + hits[i].collider.bounds.extents.y + player.collider.bounds.extents.y;
							playerState = PLAYERSTATE_DEFAULT;
							break;
						}
					}
				}
			}
			
			player.position = newPos;
		}
	}
	
	void TryClimb(float verticalDir)
	{
		//if there is a viable ladder to climb
		if(curLadder)
		{
			//if we're trying to move up or down...
			if(verticalDir != 0)
			{
				bool success = true;
				if(verticalDir < 0)
				{
					//if we're moving down, make sure there's ladder to move onto
					float detectDist = player.collider.bounds.extents.y + 0.1f;
					RaycastHit[] hits = Physics.RaycastAll(player.position, Vector3.down, detectDist);
					for(int i=0; i<hits.Length; i++)
					{
						if(hits[i].collider.tag == "LadderTrigger")
						{
							success = true;
							curLadder = hits[i].transform;
							break;
						}
					}
				}
				//start climbing, jump to the middle of the ladder
				if(success)
				{
					playerState = PLAYERSTATE_CLIMBING;
					Vector3 newPos = player.position;
					newPos.x = curLadder.position.x;
					player.position = newPos;
				}
			}
		}
	}
}
