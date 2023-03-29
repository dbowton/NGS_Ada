using ChromaSDK;
using System.Collections.Generic;
using UnityEngine;

public class RGBPlayer : MonoBehaviour
{
	public RGBController controller = new EmptyController();
	private static RGBPlayer instance = null;

	public static RGBPlayer Instance 
	{
		get 
		{ 
			if (instance == null)
			{
				GameObject ob = new GameObject();
				ob.name = "RGB Player";
				instance = ob.AddComponent<RGBPlayer>();

				if (ChromaAnimationAPI.IsChromaSDKAvailable()) instance.controller = new ChromaController();
				else if (CUE.NET.CueSDK.IsSDKAvailable()) instance.controller = new CUEController();
				else instance.controller = new EmptyController();

				instance.controller.Init();

				DontDestroyOnLoad(ob);
			}

			return instance;
		} 
	}
	public Dictionary<string, List<KeyCode>> registeredKeys = new Dictionary<string, List<KeyCode>>();

	private void LateUpdate()
	{
		controller.Update();
	}

	private void OnDisable()
	{
		controller.Shutdown();
	}
}
