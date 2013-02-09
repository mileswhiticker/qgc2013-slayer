using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	//jumping
	public float jumpDistY = 3;				//height of parabola arc
	public float jumpTimeFirst = 0.5f;		//time to reach parabola arc
	public float jumpDistFirstX = 1;		//dist to centre of parabola
	public float jumpDistSecondX = 1;		//dist to centre of parabola
	public float jumpTimeSecond = 0.5f;		//duration to reach the 'ground'
	
	public float hangTime = 0.1f;
	
	private float timeInAir = 0;
	private Vector3 playerJumpPos;
	private Vector3 playerLastPos;
	
	//public float jumpDistSecondX = 0.5f;		//dist from centre of parabola to parabola 'finish'
	//public float jumpTimeSecond = 0.5f;		//time to reach parabola 'finish'
	
	void PlayerStateProcess_Jump()
	{
		//process jumping
		timeInAir += Time.deltaTime;
		Vector3 newPos = playerLastPos;
		if(timeInAir < jumpTimeFirst + hangTime)
		{
			//parabolic xmotion
			//redundant brackets etc
			if( (curHorizDir < 0 && !CheckGeometryLeft()) || (curHorizDir > 0 && !CheckGeometryRight()) )
			{
				//newPos.x = playerJumpPos.x + curHorizDir * (jumpDistFirstX / (jumpTimeFirst * jumpTimeFirst)) * timeInAir * timeInAir;
				//linear xmotion
				newPos.x += curHorizDir * Time.deltaTime * (jumpDistFirstX / jumpTimeFirst);
			}
			if(timeInAir < jumpTimeFirst)
			{
				//parabolic ymotion
				//might be some redundant brackets in here
				newPos.y = playerJumpPos.y + (-jumpDistY / (jumpTimeFirst * jumpTimeFirst)) * ((timeInAir - jumpTimeFirst) * (timeInAir - jumpTimeFirst)) + jumpDistY;
			}
		}
		else
		{
			//finished jumping, start falling
			playerState = PLAYERSTATE_FALLING;
		}
		player.position += newPos - playerLastPos;
		playerLastPos = newPos;
	}
	
	void TryJump()
	{
		playerState = PLAYERSTATE_JUMPING;
		playerJumpPos = player.position;
		playerLastPos = player.position;
		timeInAir = 0;
	}

	//Parametric/kinematic solution (unusable)
	//http://v7.gamedev.net/topic/633112-2d-parabolic-arc-between-dynamic-a-b/
	//initial velocity = (final position - initial position) / time - 0.5 * gravity * time
	
	//Parabolic solution
	//The vertex form of a quadratic is y = a(x - h)^2 + k
	//h,k are the co-ordinates of the vertex (min/max)
	
	//LET THE EQUATION map vertical distance (y) over time (x)
	//h = jumpTimeFirst, k = jumpDistY
	//:. y = a(x - jumpTimeFirst)^2 + jumpDistY
	//substitute a point on the parabola for x and y (0,0)
	//:. 0 = a(0 - jumpTimeFirst)^2 + jumpDistY
	//:. a = -jumpDistY / (0 - jumpTimeFirst)^2
	//this equation maps a fast initial value, which slowly drops off to 0
	
	//now that we have a, solve to make an equation for y
	//:. y = (-jumpDistY / (0 - jumpTimeFirst)^2) * (x - jumpTimeFirst)^2 + jumpDistY
	//:. y = (-jumpDistY / jumpTimeFirst^2) * (x - jumpTimeFirst)^2 + jumpDistY
	
	//LET THE EQUATION map horizontal distance (y) over time (x)
	//h = 0, k = 0
	//:. y = a(x - 0)^2 + 0
	//:. y = ax^2
	//substitute a point on the parabola for x and y (jumpTimeFirst,jumpDistFirstX)
	//:. jumpDistFirstX = a * jumpTimeFirst^2
	//:. a = jumpDistFirstX / jumpTimeFirst^2
	
	//now that we have a, solve to make an equation for y
	//:. y = (jumpDistFirstX / jumpTimeFirst^2) * x^2
	//this equation maps an initial value of 0, which slowly increases to jumpDistFirstX
}
