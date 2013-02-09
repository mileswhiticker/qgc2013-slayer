using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
	
	private Transform cam;
	private Transform player;
	
	public int camTop;
	public int camLeft;
	public int camRight;
	public int camBot;
	
	public int upSpeed;
	public int downSpeed;
	public int horSpeed;
	
	void Start()
	{
		cam = Camera.main.transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update()
	{
		Vector3 tempPos = cam.position;
		if (player.position.y > (cam.position.y + camTop))
		{
			tempPos.y += upSpeed * Time.deltaTime;
		}
		if (player.position.y < (cam.position.y - camBot))
		{
			tempPos.y -= downSpeed * Time.deltaTime;
		}
		if (player.position.x > (cam.position.x + camRight))
		{
			tempPos.x += horSpeed * Time.deltaTime;
		}
		if (player.position.x < (cam.position.x - camLeft))
		{
			tempPos.x -= horSpeed * Time.deltaTime;
		}
		cam.position = tempPos;
	}
	
	void OnGUI()
	{
		
	}
}