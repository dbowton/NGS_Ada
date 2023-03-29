using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveData", menuName = "Data/Objectives/ObjectiveData")]
public class ObjectiveData : ScriptableObject
{
	[SerializeField] GameObject objectiveUIPrefab;
	private GameObject objectiveUI;

	[System.Serializable]
	public class Objective
	{
		public string key = "";
		public float current = 0f;
		public float goal = 0f;

		public Sprite sprite = null;
		public string displayName = "";
		public string description = "";
		[HideInInspector] public Action onComplete = null;
	}

	[SerializeField] List<Objective> objectives = new List<Objective>();
	private void OnValidate()
	{
		foreach (var o in objectives)
			if (string.IsNullOrWhiteSpace(o.key))
				o.sprite = null;

		for (int i = 0; i < objectives.Count - 1; i++)
			for (int j = i + 1; j < objectives.Count; j++)
				if (objectives[i].key.Equals(objectives[j].key))
					Debug.LogWarning("Warning: Multiple Objectives with key " + objectives[i].key);
	}

	public bool UpdateObjective(string key, float val)
	{
		bool foundObjective = false;
		foreach(var o in objectives) 
			if(o.key.Equals(key)) 
			{
				foundObjective = true;
				o.current += val;
				if (o.current >= o.goal && o.current - val < o.goal)
				{
					o.onComplete?.Invoke();

					if(o.sprite != null) 
					{
						Destroy(objectiveUI);

						objectiveUI = Instantiate(objectiveUIPrefab);
						DontDestroyOnLoad(objectiveUI);
						ObjectivePrefabInfo info = objectiveUI.GetComponent<ObjectivePrefabInfo>();
						info.image.sprite = o.sprite;
						info.title.text = o.displayName;
						info.description.text = o.description;
						Destroy(objectiveUI, 5f);
					}
				}
			}

		return foundObjective;
	}

	public bool IsObjectiveComplete(string key) 
	{ 
		foreach(var o in objectives)
			if(key.Equals(o.key)) 
				return (o.current >= o.goal);

		return false;
	}

	public bool AllComplete()
	{
		foreach (var o in objectives)
			if (o.current < o.goal)
				return false;

		return true;
	}

	public void ClearObjectives()
	{
		objectives.Clear();
	}

	public void RestartObjective(string key)
	{
		foreach(var o in objectives)
			if(o.key.Equals(key))
				o.current = 0f;
	}

	public void RestartObjectives()
	{
		foreach(var o in objectives)
			o.current = 0f;
	}

	public void SetFunction(string key, Action action)
	{
		foreach (var o in objectives)
			if (o.key.Equals(key))
				o.onComplete = action;
	}

	public void AddFunction(string key, Action action)
	{
		foreach (var o in objectives)
			if (o.key.Equals(key))
			{
				if(o.onComplete == null) o.onComplete = action;
				else o.onComplete += action;
			}
	}

	public bool AddObjective(string key, float goal)
	{
		foreach (var o in objectives)
			if (o.key.Equals(key) && o.goal.Equals(goal))
				return false;

		objectives.Add(new Objective { key = key, goal = goal });
		return true;
	}
}
