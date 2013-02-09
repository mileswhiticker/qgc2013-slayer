using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	//stairs
	public Transform curStairs;
	public Transform curStairsTarget;
	public float stairsDir = 0;
	
	void PlayerStateProcess_OnStairs()
	{
		if(!curStairs || !curStairsTarget)
		{
			playerState = PLAYERSTATE_DEFAULT;
			return;
		}
		
		//try jumping
		if (Input.GetKeyDown("z"))
		{
			TryJump();
		}
		
		curHorizDir = Input.GetAxis("Horizontal");
		Vector3 targetMovePos = player.position;
		if(curHorizDir < 0)
		{
			if(stairsDir < 0)
			{
				targetMovePos.y = curStairsTarget.position.y + player.collider.bounds.extents.y;
				targetMovePos.x = curStairsTarget.position.x;
			}
			else
			{
				targetMovePos.y = curStairs.position.y + player.collider.bounds.extents.y;
				targetMovePos.x = curStairs.position.x;
			}
		}
		else if(curHorizDir > 0)
		{
			if(stairsDir > 0)
			{
				targetMovePos.y = curStairsTarget.position.y + player.collider.bounds.extents.y;
				targetMovePos.x = curStairsTarget.position.x;
			}
			else
			{
				targetMovePos.y = curStairs.position.y + player.collider.bounds.extents.y;
				targetMovePos.x = curStairs.position.x;
			}
		}
		else if(Input.GetAxis("Vertical") < 0)
		{
			playerState = PLAYERSTATE_DEFAULT;
			return;
		}
		player.position = Vector3.MoveTowards(player.position, targetMovePos, moveSpeed * Time.deltaTime);
		
		float distThresholdSqrd = 0.1f * 0.1f;
		Vector3 checkPos = player.position;
		checkPos.y -= player.collider.bounds.extents.y;
		float distSqrd1 = Mathf.Abs(checkPos.sqrMagnitude - curStairs.position.sqrMagnitude);
		float distSqrd2 = Mathf.Abs(checkPos.sqrMagnitude - curStairsTarget.position.sqrMagnitude);
		//print ("dist1: " + distSqrd1 + ", dist2: " + distSqrd2 + ", threshold: " + distThresholdSqrd);
		if( (distSqrd1 < distThresholdSqrd) || (distSqrd2 < distThresholdSqrd) )
		{
			playerState = PLAYERSTATE_DEFAULT;
			//print ("reached stair end");
		}	
	}
	
	void TryStairsEnter(float vertDir, float horDir)
	{
		if(curStairs && curStairsTarget && vertDir != 0 && horDir != 0)
		{
			//make sure we're trying to enter them by moving in the right direction
			bool horizCorrect = false;
			if( ((horDir > 0) && (stairsDir > 0)) || ((horDir < 0) && (stairsDir < 0)) )
				horizCorrect = true;
			bool vertCorrect = false;
			if( ((vertDir > 0) && (curStairs.position.x > curStairsTarget.position.x)) || ((vertDir < 0) && (curStairs.position.x < curStairsTarget.position.x)) )
				vertCorrect = true;
			
			if(horizCorrect && vertCorrect)
			{
				//snap to the entry zone
				Vector3 newPos = player.position;
				newPos.x = curStairs.position.x;
				player.position = newPos;
				playerState = PLAYERSTATE_ONSTAIRS;
			}
		}
	}
}
