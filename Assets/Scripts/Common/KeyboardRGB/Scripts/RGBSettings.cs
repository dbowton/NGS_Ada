using System;
using System.Collections.Generic;
using UnityEngine;

public class RGBSettings : MonoBehaviour
{
	Dictionary<KeyCode, Timer> timers = new Dictionary<KeyCode, Timer>();

	[SerializeField] TMPro.TMP_Dropdown dropdown;
	[SerializeField] TMPro.TMP_Text debugText;

	private void Start()
	{
		debugText.text = "Select SDK";
		dropdown.ClearOptions();

		List<TMPro.TMP_Dropdown.OptionData> options = new List<TMPro.TMP_Dropdown.OptionData>
		{
			new TMPro.TMP_Dropdown.OptionData("None"),
			new TMPro.TMP_Dropdown.OptionData("Razer Chroma SDK"),
//			new TMPro.TMP_Dropdown.OptionData("Corsair CUE SDK")
		};

		dropdown.AddOptions(options);

		if (RGBPlayer.Instance.controller.GetType().IsAssignableFrom(typeof(EmptyController)))
		{
			dropdown.value = 0;
			debugText.text = "Select SDK";
		}
		else if (RGBPlayer.Instance.controller.GetType().IsAssignableFrom(typeof(ChromaController)))
		{
			dropdown.value = 1;
			debugText.text = "ChromaSDK Prepped";
		}
		else if (false && RGBPlayer.Instance.controller.GetType().IsAssignableFrom(typeof(CUEController)))
		{
			dropdown.value = 2;
			debugText.text = "CUESDK Prepped";
		}

		dropdown.onValueChanged.AddListener(ChangeController);
	}

	public void ChangeController(int val)
	{
		RGBPlayer.Instance.controller.Shutdown();

		var keys = timers.Keys;
		foreach (var key in keys)
		{
			Clock.instance?.RemoveTimer(timers[key]);
		}
		timers.Clear();

		PlayerPrefs.SetInt("RGBControllerSelection", val);
		if (val == 0)
		{
			RGBPlayer.Instance.controller = new EmptyController();
			debugText.text = "Select SDK";
		}
		else if (val == 1)
		{
			RGBPlayer.Instance.controller = new ChromaController();
			if (!RGBPlayer.Instance.controller.Init())
			{
				RGBPlayer.Instance.controller?.Shutdown();
				RGBPlayer.Instance.controller = new EmptyController();
				debugText.text = "ChromaSDK not available";
			}
			else
				debugText.text = "ChromaSDK Prepped";

		}
		else if (val == 2)
		{
			RGBPlayer.Instance.controller = new CUEController();
			if (!RGBPlayer.Instance.controller.Init())
			{
				RGBPlayer.Instance.controller?.Shutdown();
				RGBPlayer.Instance.controller = new EmptyController();
				debugText.text = "CUESDK not available";
			}
			else
				debugText.text = "CUESDK Prepped";
		}
	}

	private void Update()
	{
		RGBPlayer.Instance.controller.SetButtonColor(Colors.charcoal);

		if (Input.GetKeyDown(KeyCode.RightArrow))
			KeyboardAnimations.PlayAnimation(RGBPlayer.Instance.controller, KeyboardAnimations.Random, Color.green);
		if (Input.GetKeyDown(KeyCode.UpArrow))
			KeyboardAnimations.PlayAnimation(RGBPlayer.Instance.controller, KeyboardAnimations.Burst, Color.blue);
		if (Input.GetKeyDown(KeyCode.DownArrow))
			KeyboardAnimations.PlayAnimation(RGBPlayer.Instance.controller, KeyboardAnimations.Drop, Color.red);
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			KeyboardAnimations.PlayAnimation(RGBPlayer.Instance.controller, KeyboardAnimations.Wave, Color.yellow);
		if (Input.GetKeyDown(KeyCode.Space))
			KeyboardAnimations.StopAnimation();

		foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKey(vKey))
			{
				RGBPlayer.Instance.controller.SetKeyColor(vKey, Colors.orange);

				if (timers.ContainsKey(vKey))
					timers[vKey].Reset();
				else
					timers.Add(vKey, new Timer(1.5f,
						() =>
						{
							RGBPlayer.Instance.controller.SetKeyColor(vKey, Color.black);
							timers.Remove(vKey);
						}, true));
			}
		}
	}
}
