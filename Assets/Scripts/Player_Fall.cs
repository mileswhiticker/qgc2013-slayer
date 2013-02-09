using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	public float gravityAccel = -0.5f;
	private float fallSpeed;
	
	void PlayerStateProcess_Fall()
	{
		float tempDir = Input.GetAxis("Horizontal");
		if (curHorizDir == 0)
		{
			curHorizDir = tempDir;
		}
		
		Vector3 newPos = player.position;
		fallSpeed += gravityAccel * Time.deltaTime;
		newPos.y += fallSpeed;
		if( (curHorizDir < 0 && !CheckGeometryLeft()) || (curHorizDir > 0 && !CheckGeometryRight()) )
			newPos.x += curHorizDir * (jumpDistSecondX / jumpTimeSecond) * Time.deltaTime;
		player.position = newPos;
	}
}
