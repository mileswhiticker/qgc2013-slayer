#pragma strict

public var target : GameObject;
public var speed : int;

function Awake()
{
	target = GameObject.FindGameObjectWithTag("Player");
}

function Update()
{
	transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
}

function OnCollisionEnter()
{
	this.gameObject.active = false;
}
