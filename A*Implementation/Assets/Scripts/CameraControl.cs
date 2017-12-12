using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	

	RaycastHit hit; //declaring raycast
	bool leftClickFlag = true; //declaring leftclickflag and setting it as true by default
	public GameObject obstacle; //declaring "obstacle" gameobject to place it on the floor whenever user right clicks
	public GameObject actor;   //declaring the character which has to be assigned during run time
	public GameObject actor1;  //declaring the character which has to be assigned during run time
	public GameObject actor2;  //declaring the character which has to be assigned during run time
	public GameObject actor3;  //declaring the character which has to be assigned during run time

	public GameObject Sphere;  //declaring an object which has to be assigned during run time ,this is needed for setting the offset between the characters
	private Vector3 offset;   //declaring vector3 for defining the offset between the characters

	public string floorTag;   //defining the floor tag which has to be assigned during run time.

	Actor actorScript;
	Actor1 actor1Script;
	Actor2 actor2Script;
	Actor3 actor3Script;

	void Start()
	{
		//initially sets the offset at the starting of the game
		offset = transform.position - Sphere.transform.position;
		//checks to see if the actor gameobject was null or not 
		if (actor != null)
		{
			//if the actor gameobject is not null, then component of actor was typecasted and assigned to the actorscript
			actorScript = (Actor)actor.GetComponent(typeof(Actor));
		}
		//checks to see if the actor1 gameobject was null or not 
		if (actor1 != null)
		{
			//if the actor1 gameobject is not null, then component of actor1 was typecasted and assigned to the actor1script

			actor1Script = (Actor1)actor1.GetComponent(typeof(Actor1));
		}
		//checks to see if the actor2 gameobject was null or not 
		if (actor2 != null)
		{
			//if the actor2 gameobject is not null, then component of actor2 was typecasted and assigned to the actor2script

			actor2Script = (Actor2)actor2.GetComponent(typeof(Actor2));
		}
		//checks to see if the actor3 gameobject was null or not 
		if (actor3 != null)
		{
			//if the actor3 gameobject is not null, then component of actor3 was typecasted and assigned to the actor3script

			actor3Script = (Actor3)actor3.GetComponent(typeof(Actor3));
		}
	}

	void Update () 
	{
		
		//random code that i have written for testing
		/***Left Click***
		if (Input.GetKey(KeyCode.Mouse0) && leftClickFlag)
			leftClickFlag = false;
*/

		//when the user clicks the left mouse button 
		if (Input.GetMouseButtonDown(0))
		{
			
			leftClickFlag = true; //sets the leftclickflag to true

			//we are raycasting to the input mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				//we are also checking to see whether the click has happened on the floor or outside the floor
				if (hit.transform.tag == floorTag)
				{
					//assigns the X position of click to X
					float X = hit.point.x;
					//assigns the Z position of click to Z
					float Z = hit.point.z;
					//defining a vector3 called target which has all the co-ordinates of the click
					Vector3 target = new Vector3(X, actor.transform.position.y, Z);
					//moving the gameobject to the target(vector3)
					actorScript.MoveOrder(target);
				}
			}
		}
		//when the user clicks the left mouse button 

		if (Input.GetMouseButtonDown(0))
		{
			leftClickFlag = true;//sets the leftclickflag to true
			//we are raycasting to the input mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//point camera in direction of mouse
			if (Physics.Raycast(ray, out hit, 100))
			{
				//we are also checking to see whether the click has happened on the floor or outside the floor

				if (hit.transform.tag == floorTag)
				{
					//assigns the X position of click to X and adding a random value to make the formation

					float X = hit.point.x +(float)0.5;
					//assigns the Z position of click to Z and adding a random value to make the formation

					float Z = hit.point.z + (float)0.5;
					//defining a vector3 called target which has all the co-ordinates of the click

					Vector3 target = new Vector3(X, actor1.transform.position.y, Z);
					//moving the gameobject to the target(vector3)
				    actor1Script.MoveOrder(target);


					//random code that i have written for testing
					//transform.position = Sphere.transform.position + offset;

				}
			}
		}
		//when the user clicks the left mouse button 

		if (Input.GetMouseButtonDown(0))
		{
			leftClickFlag = true;//sets the leftclickflag to true
			//we are raycasting to the input mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//point camera in direction of mouse
			if (Physics.Raycast(ray, out hit, 100))
			{
				//we are also checking to see whether the click has happened on the floor or outside the floor

				if (hit.transform.tag == floorTag)
				{
					//assigns the X position of click to X and subtracting a random value to make the formation

					float X = hit.point.x -(float)0.5;
					//assigns the Z position of click to Z and subtracting a random value to make the formation

					float Z = hit.point.z - (float)0.5;
					//defining a vector3 called target which has all the co-ordinates of the click

					Vector3 target = new Vector3(X, actor2.transform.position.y, Z);
					//moving the gameobject to the target(vector3)

					actor2Script.MoveOrder(target);

					//random code that i have written for testing
					//transform.position = Sphere.transform.position + offset;

				}
			}
		}
		//when the user clicks the left mouse button 

		if (Input.GetMouseButtonDown(0))
		{
			leftClickFlag = true; //sets the leftclickflag to true
			//we are raycasting to the input mouse position

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//point camera in direction of mouse
			if (Physics.Raycast(ray, out hit, 100))
			{
				//we are also checking to see whether the click has happened on the floor or outside the floor

				if (hit.transform.tag == floorTag)
				{
					//assigns the X position of click to X and adding a random value to make the formation

					float X = hit.point.x +(float)0.9;
					//assigns the Z position of click to Z and subtracting a random value to make the formation

					float Z = hit.point.z -(float)0.9;
					//defining a vector3 called target which has all the co-ordinates of the click

					Vector3 target = new Vector3(X, actor3.transform.position.y, Z);
					//moving the gameobject to the target(vector3)

					actor3Script.MoveOrder(target);

					//random code that i have written for testing
					//transform.position = Sphere.transform.position + offset;

				}
			}
		}
		//to create obstacles whenever user right clicks 
		//whenever the user right clicks the mouse
		else if (Input.GetMouseButtonDown (1)) {
			//enters the loop

			RaycastHit hit; // declaring the raycast
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //point camera in direction of mouse
			if (Physics.Raycast (ray, out hit, 100)) {
				//we are also checking to see whether the click has happened on the floor or outside the floor

				if (hit.transform.tag == floorTag) {
					//we are using "INSTANTIATE" to create the obstacle at the position where user has right-clicked
					GameObject obj = Instantiate (obstacle, new Vector3 (hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
					//obj.transform.position = new Vector3 (obj.transform.position.x, 0.5f, obj.transform.position.z);
				}
			}
		}

	}


}

