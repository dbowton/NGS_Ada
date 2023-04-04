using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static T[] GetNodes<T>() where T : Node
	{
		return FindObjectsOfType<T>();
	}

	public static void ShowNodes<T>() where T : Node
	{
		var nodes = GetNodes<T>();
		nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = true);
	}

	public static void HideNodes<T>() where T : Node
	{
		var nodes = GetNodes<T>();
		nodes.ToList().ForEach(node => node.GetComponent<Renderer>().enabled = false);
	}

	public static T GetRandomNode<T>() where T : Node
	{
		var nodes = GetNodes<T>();
		return (nodes.Length == 0) ? null : nodes[Random.Range(0, nodes.Length)];
	}
}
