using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	void OnTriggerEnter(Collider col)
	{
		//curStairs
		switch(col.tag)
		{
			case("LadderTrigger"):
			{
				if(!curLadder)
				{
					curLadder = col.transform;
				}
				break;
			}
			case("Obstruction"):
			{
				//check below the player for collision
				Transform geometry = CheckGeometryBelow();
				if(geometry)
				{
					//print ("landed on geometry");
					Vector3 newPos = player.position;
					newPos.y = geometry.position.y + geometry.collider.bounds.extents.y + player.collider.bounds.extents.y;
					player.position = newPos;
					playerState = PLAYERSTATE_DEFAULT;
					tLeftNoLadderClimbing = 0.5f;
					fallSpeed = 0;
				}
			
				//for some reason these aren't setting the position properly, added appropriate checks to horizontal movement code instead
				/*
				//check left of the player for collision
				geometry = CheckGeometryLeft();
				if(geometry)
				{
					//print ("hit geometry left");
					Vector3 newPos = player.position;
					newPos.x = geometry.position.x + geometry.collider.bounds.extents.x + player.collider.bounds.extents.x;
					player.position = newPos;
				}
			
				//check right of the player for collision
				geometry = CheckGeometryRight();
				if(geometry)
				{
					//print ("hit geometry right");
					Vector3 newPos = player.position;
					newPos.x = geometry.position.x - geometry.collider.bounds.extents.x - player.collider.bounds.extents.x;
					player.position = newPos;
				}
				*/
				break;
			}
			case("LedgeTrigger"):
			{
				if(!curLedge)
				{
					curLedge = col.transform;
					ledgeDims.xMax = col.transform.position.x + col.bounds.extents.x;
					ledgeDims.xMin = col.transform.position.x - col.bounds.extents.x;
					ledgeDims.yMax = player.position.y;
					ledgeDims.yMin = col.transform.position.y - col.bounds.extents.y;
				}
				break;
			}
			case("StairsTrigger"):
			{
				curStairs = col.transform;
				StairsTrigger targetStairScript = (StairsTrigger)col.gameObject.GetComponent("StairsTrigger");
				if(targetStairScript && targetStairScript.SETME_stairsTrigger)
				{
					curStairsTarget = targetStairScript.SETME_stairsTrigger.transform;
					if(curStairsTarget)
					{
						if(curStairsTarget.position.x > player.position.x)
						{
							stairsDir = 1;
						}
						else
						{
							stairsDir = -1;
						}
					}
				}
				break;
			}
			case("StairsRampTrigger"):
			{
				if(playerState == PLAYERSTATE_FALLING)
				{
					//first, check if the trigger has been correctly linked to two stair zones
					//raycast down so we can snap to the ramp
					RaycastHit[] hits = Physics.RaycastAll(player.position, Vector3.down, player.collider.bounds.extents.y * 2);
					for(int i=0; i<hits.Length; i++)
					{
						if(hits[i].collider.tag == "StairsRampTrigger")
						{
							//work out if it's a valid ramp
							StairsTrigger_Ramp collidedRamp = (StairsTrigger_Ramp)hits[i].collider.gameObject.GetComponent("StairsTrigger_Ramp");
							if(collidedRamp && collidedRamp.SETME_stairsTrigger && collidedRamp.SETME_otherStairsTrigger)
							{
								//snap to the position of the ramp
								Vector3 newPos = player.position;
								newPos.y = hits[i].point.y + player.collider.bounds.extents.y;
								
								//change to stairs state
								playerState = PLAYERSTATE_ONSTAIRS;
								stairsDir = collidedRamp.IGNOREME_stairsDir;
								this.curStairs = collidedRamp.SETME_stairsTrigger.transform;
								this.curStairsTarget = collidedRamp.SETME_otherStairsTrigger.transform;
								break;
							}
						}
					}
				}
				break;
			}
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.transform == curLadder)
		{
			curLadder = null;
		}
		else if (col.tag == "LedgeTrigger")
		{
			curLedge = null;
		}
		else if(col.transform == curStairs)
		{
			if(playerState != PLAYERSTATE_ONSTAIRS)
			{
				curStairs = null;
				curStairsTarget = null;
			}
		}
	}
	
	Transform CheckGeometryBelow()
	{
		//check if there's a platform below the left corner
		Vector3 castPos = player.position;
		castPos.x -= player.collider.bounds.extents.x;
		RaycastHit[] hits = Physics.RaycastAll(castPos, Vector3.down, player.collider.bounds.extents.y);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				return hits[i].transform;
			}
		}
		
		//if there isn't, check for geometry below the right corner
		castPos.x += 2 * player.collider.bounds.extents.x;
		hits = Physics.RaycastAll(castPos, Vector3.down, player.collider.bounds.extents.y);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				return hits[i].transform;
			}
		}
		return null;
	}
	
	Transform CheckGeometryLeft()
	{
		//check if there's a platform left of the player top
		Vector3 castPos = player.position;
		castPos.y += player.collider.bounds.extents.y;
		RaycastHit[] hits = Physics.RaycastAll(castPos, Vector3.left, player.collider.bounds.extents.x);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				return hits[i].transform;
			}
		}
		
		//if there isn't, check for geometry below the right corner
		castPos.y -= 2 * player.collider.bounds.extents.y;
		hits = Physics.RaycastAll(castPos, Vector3.left, player.collider.bounds.extents.x);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				return hits[i].transform;
			}
		}
		return null;
	}
	
	Transform CheckGeometryRight()
	{
		//check if there's a platform left of the player top
		Vector3 castPos = player.position;
		castPos.y += player.collider.bounds.extents.y;
		RaycastHit[] hits = Physics.RaycastAll(castPos, Vector3.right, player.collider.bounds.extents.x);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				return hits[i].transform;
			}
		}
		
		//if there isn't, check for geometry below the right corner
		castPos.y -= 2 * player.collider.bounds.extents.y;
		hits = Physics.RaycastAll(castPos, Vector3.right, player.collider.bounds.extents.x);
		for(int i=0; i<hits.Length; i++)
		{
			if(hits[i].collider.tag == "Obstruction")
			{
				return hits[i].transform;
			}
		}
		return null;
	}
}
