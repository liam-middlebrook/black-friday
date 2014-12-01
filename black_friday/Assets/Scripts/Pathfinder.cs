using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AStarPathfinding
{
    class Pathfinder : MonoBehaviour
    {
        // The unique identifier for each pathfinder
        private int id;

        /// <summary>
        /// All verticies that are left to be checked
        /// </summary>
        PriorityQueue<PathNode> openSet;

        /// <summary>
        /// All verticies that have already been checked
        /// </summary>
        List<PathNode> closedSet;

        /// <summary>
        /// A use once variable for making sure the path is calculated
        /// </summary>
        private bool isPathCalculated = false;

        // The start and end points on the graph for this pathfinder
        public PathNode _start, _end;

        // The next node the pathfinder will travel to
        private PathNode nextNode;

        /// <summary>
        /// The radius at which the pathfinder will start to slow down
        /// as it approaches its target
        /// </summary>
        public float approachRadius;

        /// <summary>
        /// The maximum velocity of the pathfinder
        /// </summary>
        public float maxVelocity;

        // Get pathfinder ID
        public void Awake()
        {
            id = this.GetHashCode();
        }

        // Add pathfinder to GraphPathNode internal data
        public void Start()
        {
            PathNode.AddPathfinder(this.id);
        }

        public void Update()
        {
            // Onetime calculate the ideal path
            if (!isPathCalculated)
            {
                isPathCalculated = true;
                this.transform.position = _start.transform.position;
                AStar(_start, _end);
                nextNode = _start;
            }

            // If within range of target node
            if (Vector3.Distance(this.transform.position, nextNode) < 1.5f)
            {
                // If it's not the last node
                if (nextNode != _end)
                {
                    // Get the next node and set a random color
                    nextNode = FindNextNodeInPath(_end, nextNode);
                    this.renderer.material.color = new Color(Random.value, Random.value, Random.value);
                }
                else
                {
                    // It's the last node, end it with red!
                    this.renderer.material.color = Color.red;
                }
            }

            // Get the force direction in order to reach the next node; Apply the force
            Vector3 steerDirection = nextNode - this.transform.position;

            Vector3 desiredVelocity;

            // If we're in the approach radius start to slow down based off of distance from target
            if (steerDirection.magnitude < approachRadius)
            {
                desiredVelocity=(steerDirection.normalized * maxVelocity * (steerDirection.magnitude / approachRadius));
            }
            else
            {
                desiredVelocity = (steerDirection.normalized * maxVelocity);
            }

            // Apply the difference in velocities to the pathfinder
            this.rigidbody.AddForce(desiredVelocity - this.rigidbody.velocity);
        }
        /// <summary>
        /// Calculates the quickest distance between two points (Not ideal because of the manhattan distance hueristic)
        /// </summary>
        /// <param name="startingVertexPos">The starting position</param>
        /// <param name="goalVertexPos">The goal position</param>
        public void AStar(PathNode start, PathNode end)
        {
            PathNode startingVertex = start;
            PathNode goalVertex = end;


            //Reset the the open and closed lists of verticies
            //Add the starting vertex to the open list
            openSet = new PriorityQueue<PathNode>();
            openSet.Enqueue(startingVertex);

            closedSet = new List<PathNode>();
            PathNode currentVertex;

            //Loop through the open set until we reach the goal vertex
            while (openSet.Peek() != goalVertex)
            {
                //Set the current vertex to the first vertex in the priority queue
                currentVertex = openSet.Dequeue();
                //Move it to the closed list
                closedSet.Add(currentVertex);
                currentVertex.InOpenList[id] = false;
                currentVertex.InClosedList[id] = true;


                List<PathNode> neighbors = currentVertex.AdjacentVertices;
                //for neighbors of current
                for (int i = 0; i < neighbors.Count; i++)
                {
                    PathNode neighbor = neighbors[i];
                    //Calculate the neighbors hueristic
                    float H = Vector3.Distance(neighbor, goalVertex);
                    //Math.Abs(neighbor.X - goalVertex.X) + Math.Abs(neighbor.Y - goalVertex.Y);

                    //If it's in the closed list skip this neighbor
                    if (neighbor.InClosedList[id])
                    {
                        continue;
                    }
                    //If it's not in the open list
                    if (!neighbor.InOpenList[id])
                    {
                        //Make it in the open list
                        neighbor.InOpenList[id] = true;
                        neighbor.InClosedList[id] = false;
                        //Set its parent to the current vertex
                        neighbor.Parent[id] = currentVertex;
                        //Calculate distances
                        neighbor.G = currentVertex.G + 1;
                        neighbor.H = H;
                        neighbor.F = neighbor.G + neighbor.H;
                        //Add it to the open list
                        openSet.Enqueue(neighbor);
                    }
                    else
                    {
                        //If its not in the open list, but this path is more ideal
                        if (currentVertex.G + 1 < neighbor.G)
                        {
                            //Change its parent and recalculate distances
                            neighbor.Parent[id] = currentVertex;
                            neighbor.G = currentVertex.G + 1;
                            neighbor.F = neighbor.G + neighbor.H;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the next node in the path
        /// </summary>
        /// <param name="goal">The final node that we wish to traverse to</param>
        /// <param name="currentNode">The node we are currently on (or just passed)</param>
        /// <returns>The next node in the path</returns>
        public PathNode FindNextNodeInPath(PathNode goal, PathNode currentNode)
        {
            PathNode closestNode = goal;
            while (closestNode.Parent[id] != null && closestNode.Parent[id] != currentNode)
            {
                closestNode = closestNode.Parent[id];
            }
            return closestNode;
        }
    }
}
