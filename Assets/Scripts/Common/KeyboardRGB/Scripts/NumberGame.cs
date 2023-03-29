using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberGame : MonoBehaviour
{
	string passcode;
	string workingCode = "";
	ColorBlock colorBlock;

	[SerializeField] List<Button> buttons = new List<Button>();
	[SerializeField, Range(1, 9)] int maxNum = 4;

	private void Awake()
	{
		GenerateCode();
		colorBlock = buttons[0].colors;
		RGBPlayer.Instance.controller.SetButtonColor(Colors.orange);

		if(!RGBPlayer.Instance.registeredKeys.ContainsKey("wasd"))
			RGBPlayer.Instance.registeredKeys.Add("wasd", new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D });
	}

	private void GenerateCode()
	{
		List<string> numbers = new List<string>();
		for (int i = 0; i < 10; i++) numbers.Add(i.ToString());

		List<string> list = new List<string>();

		for (int i = 0; i < maxNum; i++)
		{
			int num = UnityEngine.Random.Range(0, numbers.Count);
			list.Add(numbers[num]);
			numbers.RemoveAt(num);
		}

		passcode = string.Join("", list);

		for (int i = 0; i < buttons.Count; i++)
		{
			int x = i;
			buttons[i].onClick.AddListener(delegate { SimulateButton(x); });
			buttons[i].GetComponentInChildren<TMPro.TMP_Text>().text = i.ToString();
		}

	}

	public void SimulateButton(int button)
	{
		CheckInput(button.ToString());
	}

	private void SetColorBlock(Color color)
	{
		colorBlock.normalColor = color;
		colorBlock.selectedColor = color;
		colorBlock.highlightedColor = color;
		colorBlock.pressedColor = color;
		colorBlock.disabledColor = color;
	}


	private void CheckInput(string number)
	{
		workingCode += number;

		if (passcode[workingCode.Length - 1].Equals(workingCode[^1]))
		{
			if (passcode.Length == workingCode.Length)
			{
				workingCode = "";

				ObjectiveManager.instance.Data.UpdateObjective("beat chroma game", 1);

				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha0, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha1, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha2, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha3, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha4, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha5, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha6, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha7, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha8, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha9, Color.yellow);

				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad0, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad1, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad2, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad3, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad4, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad5, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad6, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad7, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad8, Color.yellow);
				RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad9, Color.yellow);

				SetColorBlock(Color.yellow);
				foreach (var button in buttons)
					button.colors = colorBlock;

				new Timer(1.25f, () => 
				{ 
					RGBPlayer.Instance.controller.ClearColors();

					SetColorBlock(Color.white);
					foreach (var button in buttons)
						button.colors = colorBlock;

					GenerateCode();
					workingCode = ""; 
				}, true);
			}
			else
			{
				RGBPlayer.Instance.controller.SetKeyColor((KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + number), Color.green);
				RGBPlayer.Instance.controller.SetKeyColor((KeyCode)Enum.Parse(typeof(KeyCode), "Keypad" + number), Color.green);

				SetColorBlock(Color.green);
				buttons[int.Parse(number)].colors = colorBlock;
			}
		}
		else
		{
			workingCode = "";

			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha0, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha1, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha2, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha3, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha4, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha5, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha6, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha7, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha8, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Alpha9, Color.red);

			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad0, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad1, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad2, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad3, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad4, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad5, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad6, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad7, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad8, Color.red);
			RGBPlayer.Instance.controller.SetKeyColor(KeyCode.Keypad9, Color.red);

			SetColorBlock(Color.red);
			foreach (var button in buttons)
				button.colors = colorBlock;

			new Timer(0.125f, () => 
			{ 
				RGBPlayer.Instance.controller.ClearColors();

				SetColorBlock(Color.white);
				foreach (var button in buttons)
					button.colors = colorBlock;

				workingCode = ""; 
			}, true);
		}
	}

	private void Update()
	{
		RGBPlayer.Instance.controller.SetKeyColor(RGBPlayer.Instance.registeredKeys["wasd"], Colors.federalBlue);

		foreach (var key in Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown((KeyCode)key) && (((KeyCode)key).ToString().Contains("Alpha") || ((KeyCode)key).ToString().Contains("Keypad")))
			{
				CheckInput(((KeyCode)key).ToString()[^1..]);
			}
		}
	}
}
