using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Path pathNodes;
    public string pathName;
	public int lowerRange;
	public int upperRange;
    public Node targetNode { get; set; }

    public static Path GetPathByName(string name)
	{
        var paths = GameObject.FindObjectsOfType<Path>();
        foreach (var path in paths)
		{
            if (path.name.ToLower() == name.ToLower())
			{
                return path;
			}
		}

		print("badPath");
        return null;
	}

    public static Path GetRandomPath()
	{
        var paths = GameObject.FindObjectsOfType<Path>();

        return paths[Random.Range(0, paths.Length)];
	}

	public void Begin()
	{
	    if (pathNodes == null)
		{
			pathNodes = (pathName.Length != 0) ? GetPathByName(pathName) : GetRandomPath();
		}
	}

	public void Begin(string pathName)
	{
		this.pathName = pathName;
		Begin();
	}


	public void Move(Movement movement)
	{
		if (targetNode != null)
		{
			//movement.MoveTowards(targetNode.transform.position + new Vector3(Random.Range(lowerRange, upperRange), 0, Random.Range(lowerRange, upperRange)));
			movement.MoveTowards(targetNode.transform.position);
		}
	}
}
