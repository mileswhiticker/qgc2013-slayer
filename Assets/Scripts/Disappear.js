#pragma strict
public var other : GameObject;

function Start () 
{
	other.renderer.material.color.a = 0;
}

function Update () {

}

function OnTriggerEnter()
{
	
	other.renderer.material.color.a = 0;	
	collider.isTrigger = true;

}

function OnTriggerExit()
{
		other.renderer.material.color.a = 1.0;
		collider.isTrigger = false;
}

