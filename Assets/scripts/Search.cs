using UnityEditor;
using UnityEngine;
using Priority_Queue;
using System.Collections.Generic;
using System.Linq;

namespace Assets.scripts
{
    public class Search : MonoBehaviour
    {
        public static bool Dijkstra(AINavNode nodeStart, AINavNode nodeEnd, ref List<AINavNode> path, int maxSteps)
        {
            bool found = false;
            var nodes = new SimplePriorityQueue<AINavNode>();
            nodeStart.Cost = 0;
            nodes.EnqueueWithoutDuplicates(nodeStart, nodeStart.Cost);


            int steps = 0;
            while (!found && nodes.Count > 0 && steps++ < maxSteps)
            {
                var node = nodes.Dequeue();

                if (node == nodeEnd)
                {
                    found = true;
                    break;
                }

                foreach (var neighbor in node.neighbors)
                {
                    float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
                    if (cost < neighbor.Cost)
                    {
                        neighbor.Cost = cost;
                        neighbor.Parent = node;
                        nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost);
                    }
                }
            }
            path.Clear();
            if (found)
            {
                path = new List<AINavNode>();
                CreatePathFromParents(nodeEnd, ref path);
            }
            else
            {
                path = nodes.ToList();
            }

            return found;
        }

        public static bool AStar(AINavNode nodeStart, AINavNode nodeEnd, ref List<AINavNode> path, int maxSteps)
        {
            bool found = false;
            var nodes = new SimplePriorityQueue<AINavNode>();
            nodeStart.Cost = 0;

            float heuristic = Vector3.Distance(nodeStart.transform.position, nodeEnd.transform.position);
            nodes.EnqueueWithoutDuplicates(nodeStart, nodeStart.Cost + heuristic);

            int steps = 0;
            while (!found && nodes.Count > 0 && steps++ < maxSteps)
            {
                var node = nodes.Dequeue();

                if (node == nodeEnd)
                {
                    found = true;
                    break;
                }

                foreach (var neighbor in node.neighbors)
                {
                    float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
                    if (cost < neighbor.Cost)
                    {
                        neighbor.Cost = cost;
                        neighbor.Parent = node;
                        heuristic = Vector3.Distance(neighbor.transform.position, nodeEnd.transform.position);
                        nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost + heuristic);
                    }
                }
            }
            path.Clear();
            if (found)
            {
                path = new List<AINavNode>();
                CreatePathFromParents(nodeEnd, ref path);
            }
            else
            {
                path = nodes.ToList();
            }

            return found;
        }


        public static void CreatePathFromParents(AINavNode node, ref List<AINavNode> path)
        {
            // while node not null
            while (node != null)
            {
                // add node to list path
                path.Add(node);
                // set node to node parent
                node = node.Parent;
            }

            // reverse path
            path.Reverse();

        }















    }

}