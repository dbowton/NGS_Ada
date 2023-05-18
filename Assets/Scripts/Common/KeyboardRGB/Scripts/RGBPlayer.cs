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
				ob.name = "RGB_Player";
				instance = ob.AddComponent<RGBPlayer>();

				if (PlayerPrefs.HasKey("RGBControllerSelection"))
				{
					if (PlayerPrefs.GetInt("RGBControllerSelection") == 1 && ChromaAnimationAPI.IsChromaSDKAvailable())
						instance.controller = new ChromaController();
					else if (false && PlayerPrefs.GetInt("RGBControllerSelection") == 2 && CUE.NET.CueSDK.IsSDKAvailable())
						instance.controller = new CUEController();
				}
				else
				{
					PlayerPrefs.SetInt("RGBControllerSelection", 0);
					instance.controller = new EmptyController();
				}

				instance.controller.Init();

				DontDestroyOnLoad(ob);
			}

			return instance;
		}
	}
	public Dictionary<string, List<KeyCode>> registeredKeys = new Dictionary<string, List<KeyCode>>();

	private void Start()
	{
		SceneManagement.instance?.onSceneChange.AddListener(() =>
		{
			KeyboardAnimations.StopAnimation();
			controller.ClearAnimationColors();
			controller.ClearColors();
		});
	}

	private void LateUpdate()
	{
		controller.Update();
	}

	private void OnDisable()
	{
		controller.Shutdown();
	}

	private void OnApplicationQuit()
	{
		KeyboardAnimations.StopAnimation();
		controller.ClearAnimationColors();
		controller.ClearColors();

		controller.Update();

		controller.Shutdown();
	}
}
