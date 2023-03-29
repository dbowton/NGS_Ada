using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
	[SerializeField] ObjectiveData data;
	public ObjectiveData Data { get { return data; } }
	public static ObjectiveManager instance = null;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(this);
	}

	private void Start()
	{
		if (instance.Data.AllComplete())
			instance.Data.RestartObjectives();
	}
}
