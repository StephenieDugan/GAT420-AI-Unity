using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AINavAStar 
{
   public static bool Generate(AINavNode nodeStart, AINavNode nodeEnd, ref List<AINavNode> path)
    {
        var nodes = new SimplePriorityQueue<AINavNode>();

        nodeStart.Cost = 0;
        float heuristic = Vector3.Distance(nodeStart.transform.position, nodeEnd.transform.position);
        nodes.EnqueueWithoutDuplicates(nodeStart, nodeStart.Cost + heuristic);

        bool found = false;
        while (!found && nodes.Count > 0)
        {
            var node = nodes.Dequeue();

            if(node == nodeEnd) 
            {
                found = true;
                break;
            }

            foreach(var neighbor in node.neighbors)
            {
                float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
                if(cost < neighbor.Cost)
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
            CreatePathFromParents(nodeEnd, ref path);
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
