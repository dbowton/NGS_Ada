using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Path : MonoBehaviour
{
	[SerializeField] Node startNode;

	public Node getStartNode()
	{
		return startNode;
	}

	public Node GetRandomNode()
	{
		Node[] nodes = GetComponentsInChildren<Node>();
		Assert.IsTrue(nodes.Length != 0, "nodes not found");

		return nodes[Random.Range(0, nodes.Length)];
	}

	public Node GetNearestNode(Vector3 position)
	{
		Node[] nodes = GetComponentsInChildren<Node>();
		Assert.IsTrue(nodes.Length != 0, "nodes not found");

		Node nearestNode = null;
		float nearest = float.MaxValue;
		foreach(var node in nodes)
		{
			float distance = Vector3.Distance(position, node.transform.position);
			if (distance < nearest)
			{
				nearest = distance;
				nearestNode = node;
			}
		}
		return nearestNode;
	}
}
