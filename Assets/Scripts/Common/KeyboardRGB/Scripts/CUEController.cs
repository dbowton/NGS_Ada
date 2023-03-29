using CUE.NET;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Devices.Keyboard;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CUEController : RGBController
{
	private bool _mInitialized = false;

	CorsairKeyboard keyboard = null;

	Dictionary<CorsairLedId, (int r, int g, int b)> keyboardLights = new Dictionary<CorsairLedId, (int, int, int)>();
	Dictionary<CorsairLedId, (int r, int g, int b)> animationKeyboardLights = new Dictionary<CorsairLedId, (int, int, int)>();
	Nullable<Color> mouseLights = null;
	Nullable<Color> mouseAnimationLights = null;

	#region System Funcitons
	public override bool Init()
	{
		if (!CueSDK.IsSDKAvailable()) return false;

		CueSDK.Initialize();
		CueSDK.UpdateMode = UpdateMode.Manual;

		keyboard = CueSDK.KeyboardSDK;

		_mInitialized = true;
		return true;
	}
	public override void Update()
	{
		if (!_mInitialized) return;

		var keys = keyboardLights.Keys;
		foreach (var key in keys)
			keyboard[key].Color = new CorsairColor((byte)keyboardLights[key].r, (byte)keyboardLights[key].g, (byte)keyboardLights[key].b);

		keys = animationKeyboardLights.Keys;
		foreach (var key in keys)
			keyboard[key].Color = new CorsairColor((byte)animationKeyboardLights[key].r, (byte)animationKeyboardLights[key].g, (byte)animationKeyboardLights[key].b);
		
		keyboard.Update(true);
	}
	public override void Shutdown()
	{

	}
	#endregion

	#region Set/Get/Clear Colors
	public override void SetKeyColor(KeyCode keyCode, Color color)
	{
		CorsairLedId key = KeyConverter.KeycodeToCorsairLedId(keyCode);

		if (key == CorsairLedId.Invalid) return;

		if (keyboardLights.ContainsKey(key))
			keyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			keyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}

	public override Color GetKeyColor(KeyCode keyCode)
	{
		CorsairLedId key = KeyConverter.KeycodeToCorsairLedId(keyCode);

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
		keyboardLights.Clear();
	}
	public override void ClearButtons()
	{
		mouseLights = null;
	}
	#endregion

	#region Set/Get/Clear Animation Colors
	public override void SetKeyAnimationColor(KeyCode keyCode, Color color)
	{
		CorsairLedId key = KeyConverter.KeycodeToCorsairLedId(keyCode);

		if (key == CorsairLedId.Invalid) return;

		if (animationKeyboardLights.ContainsKey(key))
			animationKeyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			animationKeyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}
	public override Color GetKeyAnimationColor(KeyCode keyCode)
	{
		CorsairLedId key = KeyConverter.KeycodeToCorsairLedId(keyCode);

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
		animationKeyboardLights.Clear();
	}
	public override void ClearAnimationButtons()
	{
		mouseAnimationLights = null;
	}
	#endregion

	#region Get/Set Special Key Colors
	public override void SetCUEKeyColor(CUE.NET.Devices.Generic.Enums.CorsairLedId key, Color color)
	{
		if (key == CorsairLedId.Invalid) return;

		if (keyboardLights.ContainsKey(key))
			keyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			keyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}
	public override Color GetCUEKeyColor(CUE.NET.Devices.Generic.Enums.CorsairLedId key, Color color) 
	{
		if (keyboardLights.ContainsKey(key))
			return new Color(keyboardLights[key].r / 255f, keyboardLights[key].g / 255f, keyboardLights[key].b / 255f);

		return Color.black;
	}

	public override void SetCUEKeyAnimationColor(CUE.NET.Devices.Generic.Enums.CorsairLedId key, Color color) 
	{
		if (key == CorsairLedId.Invalid) return;

		if (animationKeyboardLights.ContainsKey(key))
			animationKeyboardLights[key] = ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255));
		else
			animationKeyboardLights.Add(key, ((int)(color.r * 255), (int)(color.g * 255), (int)(color.b * 255)));
	}
	public override Color GetCUEKeyAnimationColor(CUE.NET.Devices.Generic.Enums.CorsairLedId key, Color color) 
	{
		if (animationKeyboardLights.ContainsKey(key))
			return new Color(animationKeyboardLights[key].r / 255f, animationKeyboardLights[key].g / 255f, animationKeyboardLights[key].b / 255f);

		return Color.black;
	}
	#endregion
}
