#pragma strict


public var smokeBomb : Transform;

function Teleport()
{
	transform.position = Vector3 (-10,-26,0);
	Instantiate (smokeBomb, transform.position, transform.rotation);
}

function Start()
{
	InvokeRepeating("Teleport", 1, 9);
	InvokeRepeating("Teleport2", 1,4);
	InvokeRepeating("Teleport3", 1,5);
	InvokeRepeating("Teleport4", 1,6);
	InvokeRepeating("Teleport5", 1,7);
	InvokeRepeating("Teleport6", 1,8);
}

function Teleport2()
{
	transform.position = Vector3 (-4,-26,0);
	Instantiate (smokeBomb, transform.position, transform.rotation);
}

function Teleport3()
{
	transform.position = Vector3 (7,-26,0);
	Instantiate (smokeBomb, transform.position, transform.rotation);
}

function Teleport4()
{
	transform.position = Vector3 (10,-26,0);
	Instantiate (smokeBomb, transform.position, transform.rotation);
}
function Teleport5()
{
	transform.position = Vector3 (3,-26,0);
	Instantiate (smokeBomb, transform.position, transform.rotation);
}

function Teleport6()
{
	transform.position = Vector3 (1,-26,0);
	Instantiate (smokeBomb, transform.position, transform.rotation);
}
