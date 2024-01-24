using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AINavPath : MonoBehaviour
{
	//[RequireComponent(typeof(AINavAgent))]
	[SerializeField]enum ePathType
	{ 
		WayPoint,
		Dijkstra,
		AStar
	}

	[SerializeField] ePathType pathType;
	[SerializeField] AINavNode startNode;
	[SerializeField] AINavNode endNode;

	AINavAgent agent;
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
				generatePath(startNode, endNode);
			}
		}
	}

	private void Start()
	{
		agent = GetComponent<AINavAgent>();
		targetNode = (startNode != null) ? startNode : AINavNode.GetRandomAINavNode(); 
		
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
		AINavDijkstra.Generate(startnode, endnode, ref path);
	}

	private AINavNode getNextPathAINavNode(AINavNode node)
	{
		if(path.Count == 0) return node;

		int index = path.FindIndex(pathNode => pathNode == node);

		if(index == -1) return node;
		if(index+1 == path.Count) return null;

		AINavNode nextNode = path[index+1];

		return null;
	}
}
