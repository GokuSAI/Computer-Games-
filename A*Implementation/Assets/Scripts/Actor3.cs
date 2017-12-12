using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor3 : MonoBehaviour {

	//we are using enum keyword to declare enumeration,which is nothing but a set of values.here we have only two states, IDLE and the MOVING state
	enum State
	{
		IDLE,
		MOVING,
	}

	float m_speed;  //declaring a float variable m_speed
	float m_speed_multi = 5;  //declaring a float variable m_speed
	public bool DebugMode;    //declaring a boolean variable DebugMode

	bool onNode = true;     //decalring boolean onNode variable and setting its value to true
	Vector3 m_target = new Vector3(0, 0, 0); // declaring vector m_target
	Vector3 currNode;						//declaring another vector currNode
	int nodeIndex;    //declaring integer variable nodeIndex
	List<Vector3> path = new List<Vector3>();  
	NodeControl control;
	State state = State.IDLE;
	float OldTime = 0;
	float checkTime = 0;
	float elapsedTime = 0;

	//defining a function called Awake()
	void Awake()
	{
		//finding the GameObject with tag "MainCamera" and assiging it to cam
		GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
		control = (NodeControl)cam.GetComponent(typeof(NodeControl));
	}

	//Update function which gets called every frame
	void Update () 
	{

		m_speed = Time.deltaTime * m_speed_multi; //calculating m_speed
		elapsedTime += Time.deltaTime;     //adding Time.deltaTime to elapsedTime

		//checking to see if elapsedTime is greater than OldTime
		if (elapsedTime > OldTime)
		{
			//if true, enter the loop
			//checking the state by using switch statement
			switch (state)
			{

			case State.IDLE:
				//if the state is IDLE, then break
				break;

			case State.MOVING:
				//if the state is moving then calculate OldTime
				OldTime = elapsedTime + 0.01f;

				//checking to see if elapsedTime is greater than checkTime

				if (elapsedTime > checkTime)
				{
					//if true, then calculate the checkTime
					checkTime = elapsedTime + 1;
					//calling the setTarget() function
					SetTarget();
				}

				//checking to see if the path is null or not
				if (path != null)
				{
					//enter if the path is not null
					if (onNode)

					{
						//if onNode is true then enter the loop and set onNode to false
						onNode = false;
						//if condition to check whether nodeIndex is less than path.count
						if (nodeIndex < path.Count)
							//if it is true, then calculate the currNode
							currNode = path[nodeIndex];
					} else
						//if it is false ,call the MoveToward() function
						MoveToward();
				}
				break;
			}
		}

	}
	//defining a function called MoveToward()
	void MoveToward()
	{
		//if condition to check whether the DebugMode is on or not
		if (DebugMode)

		{
			//enter the loop, if the DebugMode is on
			for (int i=0; i<path.Count-1; ++i)
			{
				//code to draw the line in the direction of movement 
				Debug.DrawLine((Vector3)path[i], (Vector3)path[i+1], Color.white, 0.01f);
			}
		}
		//defining the newPos as the transform.position
		Vector3 newPos = transform.position;
		//calculating the Xdistance
		float Xdistance = newPos.x - currNode.x;
		if (Xdistance < 0) Xdistance -= Xdistance*2;
		//calculating the Ydistance
		float Ydistance = newPos.z - currNode.z;
		if (Ydistance < 0) Ydistance -= Ydistance*2;
		//if condition to check whether we have reached Target or not
		if ((Xdistance < 0.1 && Ydistance < 0.1) && m_target == currNode) //Reached target
		{
			//if true, change the state to idle
			ChangeState(State.IDLE);
		}
		else if (Xdistance < 0.1 && Ydistance < 0.1)
		{
			//if it is false, increment nodeIndex
			nodeIndex++;
			//set onNode to true
			onNode = true;
		}

		/***Move toward waypoint***/
		Vector3 motion = currNode - newPos;
		motion.Normalize();
		newPos += motion * m_speed;

		transform.position = newPos;
	}
	//defining a function called setTarget() 
	private void SetTarget()
	{
		//defining path 
		path = control.Path(transform.position, m_target);
		//setting nodeIndex to 0
		nodeIndex = 0;
		//setting onNode to true
		onNode = true;
	}
	//defining a function called MoveOrder() 

	public void MoveOrder(Vector3 pos)
	{
		//assigning pos to m_target
		m_target = pos;
		//calling setTarget()
		SetTarget();
		//changing the state from IDLE to Moving
		ChangeState(State.MOVING);
	}
	//defining a function called ChangeState() 

	private void ChangeState(State newState)
	{
		//assigning the newState
		state = newState;
	}
}
