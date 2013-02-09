using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public Texture norm;
	public Texture hover;
	public Texture clicked;
	
	private Transform button;
	
	private int buttonState = 0;
	
	private const int BUTTON_NORM = 0;
	private const int BUTTON_HOVER = 1;
	private const int BUTTON_CLICKED = 2;
	

	
	// Use this for initialization
	void Start () 
	{
		button = this.transform;
		hover = Resources.Load("hover") as Texture;
		norm = Resources.Load("norm") as Texture;
		clicked = Resources.Load("click") as Texture;
		button.renderer.material.mainTexture = norm;
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray,out hit,100F))
		{
			if (hit.collider.tag == "Play")
			{
				//if hovering
				hit.collider.renderer.material.mainTexture = hover;
				if (Input.GetMouseButtonDown(0))
				{
					button.renderer.material.mainTexture = clicked;
					Application.LoadLevel(1);
				}
			}
			
		}
		else
		{
			button.renderer.material.mainTexture = norm;
		}
	}
}
