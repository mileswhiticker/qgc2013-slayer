using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	//ledges
	public float ledgeClimbSpeed = 3;
	public float ledgeCrawlSpeed = 4;
	private Transform curLedge;
	private Rect ledgeDims;
	
	void PlayerStateProcess_Ledgecrawl()
	{
		curHorizDir = Input.GetAxis("Horizontal");
		if (curHorizDir != 0)
		{
			Vector3 newPos = player.position;
			newPos.x += curHorizDir * ledgeCrawlSpeed * Time.deltaTime;
			
			//check position in relation to edges of ledge
			if ((player.position.x + player.collider.bounds.extents.x) > ledgeDims.xMax)
			{
				newPos.x = ledgeDims.xMax - player.collider.bounds.extents.x;
			}
			else if ((player.position.x - player.collider.bounds.extents.x) < ledgeDims.xMin)
			{
				newPos.x = ledgeDims.xMin + player.collider.bounds.extents.x;
			}
			player.position = newPos;
		}
		float tempVertDir = Input.GetAxis("Vertical");
		if(tempVertDir > 0)
		{
			//check for anything blocking above
			bool clearAbove = true;
			float detectDist = player.collider.bounds.extents.y;
			
			//check left corner
			Vector3 edgeOffset = new Vector3(-player.collider.bounds.extents.x, player.collider.bounds.extents.y, 0);
			RaycastHit[] hits = Physics.RaycastAll(player.position + edgeOffset, Vector3.up, detectDist);
			for(int i=0; i<hits.Length; i++)
			{
				if(hits[i].collider.tag == "Obstruction")
				{
					clearAbove = false;
					break;
				}
			}
			
			if(clearAbove)
			{
				//if it was clear, check right corner
				edgeOffset = new Vector3(player.collider.bounds.extents.x, player.collider.bounds.extents.y, 0);
				hits = Physics.RaycastAll(player.position + edgeOffset, Vector3.up, detectDist);
				for(int i=0; i<hits.Length; i++)
				{
					if(hits[i].collider.tag == "Obstruction")
					{
						clearAbove = false;
						break;
					}
				}
			
				//if it was clear, we can move upwards
				if(clearAbove)
				{
					//print ("climb up from a ledge");
					forceMoveSpeed = ledgeClimbSpeed;
					forceMovePos = new Vector3(player.position.x, ledgeDims.yMax, player.position.z);
					playerState = PLAYERSTATE_FORCEMOVE;
					nextPlayerState = PLAYERSTATE_DEFAULT;
				}
				else
				{
					//print ("climb up edge blocked");
				}
			}
		
		}
		else if(tempVertDir < 0)
		{
			//drop off the ledge
			playerState = PLAYERSTATE_DEFAULT;
		}
	}
	
	void TryLedgeDrop()
	{
		if(curLedge)
		{
			if( ledgeDims.xMin <= (player.position.x - player.collider.bounds.extents.x) && ledgeDims.xMax >= (player.position.x + player.collider.bounds.extents.x) )
			{
				forceMoveSpeed = ledgeClimbSpeed;
				forceMovePos = new Vector3(player.position.x, ledgeDims.yMin - player.collider.bounds.extents.y, player.position.z);
				playerState = PLAYERSTATE_FORCEMOVE;
				nextPlayerState = PLAYERSTATE_LEDGECRAWL;
			}
		}
	}
}
