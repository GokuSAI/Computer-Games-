using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeControl : MonoBehaviour {

	public string nodeTag; //decalring a public string variable 

	class Point
	{
		Vector3 m_pos; //declaring a vector3
		char m_state = 'u'; //declaring m_state
		float m_score = 0;  //declaring float variable m_score
		Point m_prevPoint;  //declaring a point m_prevpoint

		List<Point> m_connectedPoints = new List<Point>(); //declaring a list 
		List<Point> m_potentialPrevPoints = new List<Point>(); //declaring another list 

		public Point(Vector3 pos, char state = 'u')
		{
			m_pos = pos;
			m_state = state;
		}

		//function which return the state
		public char GetState()
		{
			return m_state;
		}
		//function which return the position
		public Vector3 GetPos()
		{
			return m_pos;
		}
		//function which the List of points
		public List<Point> GetConnectedPoints()
		{
			return m_connectedPoints;
		}
		//function to return the previous points
		public Point GetPrevPoint()
		{
			return m_prevPoint;
		}
		//function to return the score
		public float GetScore()
		{
			return m_score;
		}
		//function which return the previous points list
		public List<Point> GetPotentialPrevPoints()
		{
			return m_potentialPrevPoints;
		}
		//function to add points to the connected points list
		public void AddConnectedPoint(Point point)
		{
			m_connectedPoints.Add(point);
		}
		//function to add a point to the previous points list
		public void AddPotentialPrevPoint(Point point)
		{
			m_potentialPrevPoints.Add(point);
		}
		//function to se the previous point
		public void SetPrevPoint(Point point)
		{
			m_prevPoint = point;
		}
		//function to set the state 
		public void SetState(char newState)
		{
			m_state = newState;
		}
		//function to set the score
		public void SetScore(float newScore)
		{
			m_score = newScore;
		}
	}

	public List<Vector3> Path(Vector3 startPos, Vector3 targetPos)
	{
		//for checking the gap between the obstacles
		float exitDistance = Vector3.Distance(startPos, targetPos);
		if (exitDistance > .7f)
			exitDistance -= .7f;
		if (!Physics.Raycast(startPos, targetPos - startPos, exitDistance))
		{
			List<Vector3> path = new List<Vector3>();
			path.Add(startPos);
			path.Add(targetPos);
			return path;
		}
		//finding the gameobject with tag nodetag
		GameObject[] nodes = GameObject.FindGameObjectsWithTag(nodeTag);
		List<Point> points = new List<Point>();
		//writing for each loop for all the nodes
		foreach (GameObject node in nodes)
		{
			//adding the currNode to points list
			Point currNode = new Point(node.transform.position);
			points.Add(currNode);
		}

		Point endPoint = new Point(targetPos, 'e');

		/***Connect them together***/
		foreach(Point point in points) //for each loop for looping through the points list
		{
			foreach (Point point2 in points) 
			{
				float distance = Vector3.Distance(point.GetPos(), point2.GetPos());
				if (!Physics.Raycast(point.GetPos(), point2.GetPos() - point.GetPos(), distance))
				{
					//Debug.DrawRay(point.GetPos(), point2.GetPos() - point.GetPos(), Color.white, 1);
					//adding the point2 to the point
					point.AddConnectedPoint(point2);
				}
			}
			float distance2 = Vector3.Distance(targetPos, point.GetPos());
			if (!Physics.Raycast(targetPos, point.GetPos() - targetPos, distance2))
			{
				//Debug.DrawRay(targetPos, point.GetPos() - targetPos, Color.white, 1);
				//adding the endPoint to the point
				point.AddConnectedPoint(endPoint);
			}
		}

		//points startPos can see
		foreach (Point point in points) // another for each loop which loops through the point list
		{
			float distance = Vector3.Distance(startPos, point.GetPos());
			if (!Physics.Raycast(startPos, point.GetPos() - startPos, distance))
			{
				//Debug.DrawRay(startPos, point.GetPos() - startPos, Color.white, 1);
				Point startPoint = new Point(startPos, 's');
				//setting the previous point 
				point.SetPrevPoint(startPoint);
				//setting the state
				point.SetState('o');
				//setting the score
				point.SetScore(distance + Vector3.Distance(targetPos, point.GetPos()));
			}
		}

		//Go through until we find the exit or run out of connections
		bool searchedAll = false;
		bool foundEnd = false;
		//checking searchedAll is false or not
		while(!searchedAll)
		{
			//enters if while loop condition is true
			searchedAll = true;
			List<Point> foundConnections = new List<Point>();
			foreach (Point point in points)
			{
				//if ocndition to see whether getState is 'o' or not
				if (point.GetState() == 'o')
				{
					//setting searchedAll to false
					searchedAll = false;
					List<Point> potentials = point.GetConnectedPoints();

					foreach (Point potentialPoint in potentials)
					{
						//if ocndition to see whether getState is 'u' or not
						if (potentialPoint.GetState() == 'u')
						{
							//adding the point to potentialPoint 
							potentialPoint.AddPotentialPrevPoint(point);
							//also adding it to the foundconnections
							foundConnections.Add(potentialPoint);
							//setting the score 
							potentialPoint.SetScore(Vector3.Distance(startPos, potentialPoint.GetPos()) + Vector3.Distance(targetPos, potentialPoint.GetPos()));
						} else if (potentialPoint.GetState() == 'e')
						{
							//Found the exit and assign foundEnd to true
							foundEnd = true;
							//adding the point to endpoint list
							endPoint.AddConnectedPoint(point);
						}
					}
					//setting the state
					point.SetState('c');
				}
			}
			foreach (Point connection in foundConnections)
			{

			//setting the state to 'o' again
				connection.SetState('o');
				//Find lowest scoring prev point
				float lowestScore = 0;  //declaring lowestScore as 0
				Point bestPrevPoint = null;
				bool first = true;
				foreach (Point prevPoints in connection.GetPotentialPrevPoints())
				{
					if (first)
					{
						//if condition is true , then enter
						//assign lowestScore from prevPoints.getScore()
						lowestScore = prevPoints.GetScore();
						bestPrevPoint = prevPoints;
					//set first to false
						first = false;
					} else
					{
						if (lowestScore > prevPoints.GetScore())
						{
							//assign lowestScore from prevPoints.getScore()

							lowestScore = prevPoints.GetScore();
							bestPrevPoint = prevPoints;
						}
					}
				}
				//setting the previous point 
				connection.SetPrevPoint(bestPrevPoint);
			}
		}

		if (foundEnd)
		{
			//trace back finding shortest route (lowest score)
			List<Point> shortestRoute = null;
			float lowestScore = 0; //declaring lowestscore
			bool firstRoute = true;

			foreach (Point point in endPoint.GetConnectedPoints())
			{
				float score = 0;
				bool tracing = true;
				Point currPoint = point;
				List<Point> route = new List<Point>(); //defining a list called route
				//adding the endPoint to route list
				route.Add(endPoint);
				while(tracing)
				{
					//if condition is true, then enter
					//add currPoint to route list
					route.Add(currPoint);
					//check the state is 's' or not
					if (currPoint.GetState() == 's')
					{
						//enter if true
						if (firstRoute)
						{
							//enter only when if condition is true
							shortestRoute = route;
							lowestScore = score;
							//set firstRoute to false
							firstRoute = false;
						} else
						{
							//check if lowestscore is less than score
							if (lowestScore > score)
							{
								//assign the route to shortestroute
								shortestRoute = route;
								lowestScore = score;
							}
						}
						//set tracing to false and break 
						tracing = false;
						break;
					}
					//calculat the score and currPoint
					score += currPoint.GetScore();
					currPoint = currPoint.GetPrevPoint();
				}
			}

			shortestRoute.Reverse();
			List<Vector3> path = new List<Vector3>(); //defining another list called path
			foreach (Point point in shortestRoute)  //for each loop which loops shortestRoute list
			{
				//add the point.getpos() to the path list
				path.Add(point.GetPos());
			}
			//return the path list
			return path;
		} else
		{
			//if not true, return null
			return null;
		}
	}
}
