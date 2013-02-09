using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	void PlayerStateProcess_Default()
	{
		//BASIC MOVEMENT
		curHorizDir = Input.GetAxis("Horizontal");
		if( (curHorizDir < 0 && !CheckGeometryLeft()) || (curHorizDir > 0 && !CheckGeometryRight()) )
		{
			Vector3 newPos = player.position;
			newPos.x += curHorizDir * moveSpeed * Time.deltaTime;
			player.position = newPos;
		}
		
		//see if the player is trying to get onto stairs / ledges
		float curVertDir = Input.GetAxis("Vertical");
		if(curVertDir != 0)
		{
			//trying to climb stairs
			TryStairsEnter(curVertDir, curHorizDir);
		
			//trying to drop onto ledge
			TryLedgeDrop();
		}
		
		//try jumping
		if (Input.GetKeyDown("z"))
		{
			TryJump();
		}
		
		//see if we walked off an edge
		if(!CheckGeometryBelow())
		{
			//if there isn't, start falling
			playerState = PLAYERSTATE_FALLING;
		}
	}
}
