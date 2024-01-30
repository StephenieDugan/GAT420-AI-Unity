using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AINavAgent))]
public class AINavPath : MonoBehaviour
{
	[SerializeField]enum ePathType
	{ 
		WayPoint,
		Dijkstra,
		AStar
	}

	[SerializeField] AINavAgent agent;
	[SerializeField] ePathType pathType;

	List<AINavNode> path = new List<AINavNode>();

	public AINavNode targetNode { get; set; } = null;
	public Vector3 destination 
	{ 
		get 
		{ 
			return (targetNode != null) ? targetNode.transform.position : Vector3.zero; 
		} 
		set
		{
			if(pathType == ePathType.WayPoint){targetNode = agent.GetNearestAINavNode();}
			else if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar)
			{
				AINavNode startNode = agent.GetNearestAINavNode();
				AINavNode endNode = agent.GetNearestAINavNode(value);
				generatePath(startNode, endNode);
				targetNode = startNode;
			}
		}
	}

	

	public bool HasTarget()
	{
		return targetNode != null;
	}

	public AINavNode GetNextAINavNode(AINavNode node)
	{
		if (pathType == ePathType.WayPoint)	return node.GetRandomNeighbor();
		if(pathType == ePathType.Dijkstra || pathType == ePathType.AStar) return getNextPathAINavNode(node);

		return null;
	}

	private void generatePath(AINavNode startnode, AINavNode endnode)
	{
		AINavNode.ResetNodes();
		if(pathType == ePathType.Dijkstra) {AINavDijkstra.Generate(startnode, endnode, ref path); }
		if(pathType == ePathType.AStar) { AINavAStar.Generate(startnode, endnode, ref path); }
		
	}

	private AINavNode getNextPathAINavNode(AINavNode node)
	{
		if(path.Count == 0) return node;
		agent.GetComponent<AIKinematicMovement>().maxForce = 1;
		int index = path.FindIndex(pathNode => pathNode == node);

		if(index == -1 ||index+1 == path.Count) return null;

		AINavNode nextNode = path[index+1];

		return nextNode;
	}

    private void OnDrawGizmosSelected()
    {
        if (path.Count == 0) return;

        var pathArray = path.ToArray();

        for (int i = 1; i < path.Count - 1; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(pathArray[i].transform.position + Vector3.up, 1);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pathArray[0].transform.position + Vector3.up, 1);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pathArray[pathArray.Length - 1].transform.position + Vector3.up, 1);
    }
}
