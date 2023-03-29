using ChromaSDK;
using System;
using System.Collections.Generic;
using UnityEngine;
using static ChromaSDK.ChromaAnimationAPI;

public class ChromaController : RGBController
{
	private bool _mInitialized = false;
	private int _mResult = 0;

	int[] colorsKeyboard;
	int[] colorsMouse;

	Dictionary<Keyboard.RZKEY, (int r, int g, int b)> keyboardLights = new Dictionary<Keyboard.RZKEY, (int, int, int)>();
	Dictionary<Keyboard.RZKEY, (int r, int g, int b)> animationKeyboardLights = new Dictionary<Keyboard.RZKEY, (int, int, int)>();
	Nullable<Color> mouseLights = null;
	Nullable<Color> mouseAnimationLights = null;

	#region System Functions
	public override bool Init()
	{
		if (!ChromaAnimationAPI.IsChromaSDKAvailable()) return false;

		ChromaSDK.APPINFOTYPE appInfo = new APPINFOTYPE();
		appInfo.Title = "Razer Chroma Unity Game Loop Sample Application";
		appInfo.Description = "A sample application using Razer Chroma SDK";

		appInfo.Author_Name = "Razer";
		appInfo.Author_Contact = "https://developer.razer.com/chroma";

		appInfo.SupportedDevice = (0x01 | 0x02);
		appInfo.Category = 1;
		_mResult = ChromaAnimationAPI.InitSDK(ref appInfo);

		if (_mResult != RazerErrors.RZRESULT_SUCCESS) return false;

		int sizeKeyboard = GetColorArraySize2D(Device2D.Keyboard);
		int sizeMouse = GetColorArraySize2D(Device2D.Mouse);

		colorsKeyboard = new int[sizeKeyboard];
		colorsMouse = new int[sizeMouse];

		_mInitialized = true;
		return true;
	}
	public override void Update()
	{
		if (!_mInitialized) return;

		var keys = keyboardLights.Keys;
		foreach (var key in keys)
			SetKeyColorRGB(colorsKeyboard, (int)key, keyboardLights[key].r, keyboardLights[key].g, keyboardLights[key].b);

		keys = animationKeyboardLights.Keys;
		foreach (var key in keys)
			SetKeyColorRGB(colorsKeyboard, (int)key, animationKeyboardLights[key].r, animationKeyboardLights[key].g, animationKeyboardLights[key].b);

		if (mouseAnimationLights != null)
			Array.Fill(colorsMouse, ChromaAnimationAPI.GetRGB((int)(((Color)mouseAnimationLights).r * 255), (int)(((Color)mouseAnimationLights).g * 255), (int)(((Color)mouseAnimationLights).b * 255)));
		else if (mouseLights != null)
			Array.Fill(colorsMouse, ChromaAnimationAPI.GetRGB((int)(((Color)mouseLights).r * 255), (int)(((Color)mouseLights).g * 255), (int)(((Color)mouseLights).b * 255)));

		ChromaAnimationAPI.SetCustomColorFlag2D((int)Device2D.Keyboard, colorsKeyboard);
		ChromaAnimationAPI.SetEffectKeyboardCustom2D((int)Device2D.Keyboard, colorsKeyboard);

		ChromaAnimationAPI.SetEffectCustom2D((int)Device2D.Mouse, colorsMouse);
	}
	public override void Shutdown()
	{
		if (_mResult == RazerErrors.RZRESULT_SUCCESS)
		{
			ChromaAnimationAPI.StopAll();
			ChromaAnimationAPI.CloseAll();
			int result = ChromaAnimationAPI.Uninit();

#if !UNITY_EDITOR
            ChromaAnimationAPI.UninitAPI();
#endif
		}
	}
	#endregion

	#region Set/Get/Clear Colors
	public override void SetKeyColor(KeyCode keyCode, Color color)
	{
		Keyboard.RZKEY key = KeyConverter.KeycodeToRZKey(keyCode);

		if (key == Keyboard.RZKEY.RZKEY_INVALID) return;

		if (keyboardLights.ContainsKey(key))
			keyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			keyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}
	public override Color GetKeyColor(KeyCode keyCode)
	{
		Keyboard.RZKEY key = KeyConverter.KeycodeToRZKey(keyCode);

		if (keyboardLights.ContainsKey(key))
			return new Color(keyboardLights[key].r / 255f, keyboardLights[key].g / 255f, keyboardLights[key].b / 255f);

		return Color.black;
	}

	public override void SetButtonColor(Color color)
	{
		mouseLights = color;
	}
	public override Color GetButtonColor()
	{
		if (mouseLights == null)
			return Color.black;
		else
			return (Color)mouseLights;
	}

	public override void ClearKeys()
	{
		Array.Clear(colorsKeyboard, 0, colorsKeyboard.Length);
		keyboardLights.Clear();
	}
	public override void ClearButtons()
	{
		Array.Clear(colorsMouse, 0, colorsMouse.Length);
		mouseLights = null;
	}
	#endregion

	#region Set/Get/Clear Animation Colors
	public override void SetKeyAnimationColor(KeyCode keyCode, Color color)
	{
		Keyboard.RZKEY key = KeyConverter.KeycodeToRZKey(keyCode);

		if (key == Keyboard.RZKEY.RZKEY_INVALID) return;

		if (animationKeyboardLights.ContainsKey(key))
			animationKeyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			animationKeyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}
	public override Color GetKeyAnimationColor(KeyCode keyCode)
	{
		Keyboard.RZKEY key = KeyConverter.KeycodeToRZKey(keyCode);

		if (animationKeyboardLights.ContainsKey(key))
			return new Color(animationKeyboardLights[key].r / 255f, animationKeyboardLights[key].g / 255f, animationKeyboardLights[key].b / 255f);

		return Color.black;
	}

	public override void SetButtonAnimationColor(Color color)
	{
		mouseAnimationLights = color;
	}
	public override Color GetButtonAnimationColor()
	{
		if (mouseAnimationLights == null)
			return Color.black;
		else
			return (Color)mouseAnimationLights;
	}

	public override void ClearAnimationKeys()
	{
		Array.Clear(colorsKeyboard, 0, colorsKeyboard.Length);
		animationKeyboardLights.Clear();
	}
	public override void ClearAnimationButtons()
	{
		Array.Clear(colorsMouse, 0, colorsMouse.Length);
		mouseAnimationLights = null;
	}
	#endregion

	#region Set/Get Special Key Colors
	public override void SetChromaKeyColor(ChromaSDK.Keyboard.RZKEY key, Color color) 
	{
		if (key == Keyboard.RZKEY.RZKEY_INVALID) return;

		if (keyboardLights.ContainsKey(key))
			keyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			keyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}	
	public override Color GetChromaKeyColor(ChromaSDK.Keyboard.RZKEY key, Color color)
	{
		if (keyboardLights.ContainsKey(key))
			return new Color(keyboardLights[key].r / 255f, keyboardLights[key].g / 255f, keyboardLights[key].b / 255f);

		return Color.black;
	}

	public override void SetChromaKeyAnimationColor(ChromaSDK.Keyboard.RZKEY key, Color color) 
	{
		if (key == Keyboard.RZKEY.RZKEY_INVALID) return;

		if (animationKeyboardLights.ContainsKey(key))
			animationKeyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			animationKeyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}
	public override Color GetChromaKeyAnimationColor(ChromaSDK.Keyboard.RZKEY key, Color color)
	{
		if (animationKeyboardLights.ContainsKey(key))
			return new Color(animationKeyboardLights[key].r / 255f, animationKeyboardLights[key].g / 255f, animationKeyboardLights[key].b / 255f);

		return Color.black;
	}
	#endregion

	int GetColorArraySize2D(Device2D device)
	{
		int maxRow = ChromaAnimationAPI.GetMaxRow(device);
		int maxColumn = ChromaAnimationAPI.GetMaxColumn(device);
		return maxRow * maxColumn;
	}
	void SetKeyColorRGB(int[] colors, int rzkey, int red, int green, int blue)
	{
		int row = (rzkey & 0xFF00) >> 8;
		int column = (rzkey & 0x00FF);

		int index = ChromaAnimationAPI.GetMaxColumn(Device2D.Keyboard) * row + column;

		colors[index] = ChromaAnimationAPI.GetRGB(red, green, blue);
	}
}
