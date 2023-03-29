using System.Collections.Generic;
using UnityEngine;

public abstract class RGBController
{
	#region System Functions
	public abstract bool Init();
	public abstract void Update();
	public abstract void Shutdown();
	#endregion

	#region Get/Set/Clear Colors

	public void SetKeyColor(List<KeyCode> keys, Color color) { foreach (var key in keys) SetKeyColor(key, color); }

	public abstract void SetKeyColor(KeyCode keyCode, Color color);
	public abstract Color GetKeyColor(KeyCode keyCode);

	public abstract void SetButtonColor(Color color);
	public abstract Color GetButtonColor();

	public void ClearColors() { ClearKeys(); ClearButtons(); }
	public abstract void ClearKeys();
	public abstract void ClearButtons();
	#endregion

	#region Get/Set/Clear Animation Colors
	public void SetKeyAnimationColor(List<KeyCode> keys, Color color) { foreach (var key in keys) SetKeyAnimationColor(key, color); }
	public abstract void SetKeyAnimationColor(KeyCode keyCode, Color color);
	public abstract Color GetKeyAnimationColor(KeyCode keyCode);

	public abstract void SetButtonAnimationColor(Color color);
	public abstract Color GetButtonAnimationColor();

	public void ClearAnimationColors() { ClearAnimationKeys(); ClearAnimationButtons(); }
	public abstract void ClearAnimationKeys();
	public abstract void ClearAnimationButtons();
	#endregion

	#region Get/Set Special Key Colors
	public virtual void SetChromaKeyColor(ChromaSDK.Keyboard.RZKEY key, Color color) { }
	public virtual void SetCUEKeyColor(CUE.NET.Devices.Generic.Enums.CorsairLedId  key, Color color) { }
	public virtual Color GetChromaKeyColor(ChromaSDK.Keyboard.RZKEY key, Color color) { return Color.black; }
	public virtual Color GetCUEKeyColor(CUE.NET.Devices.Generic.Enums.CorsairLedId key, Color color) { return Color.black; }



	public virtual void SetChromaKeyAnimationColor(ChromaSDK.Keyboard.RZKEY key, Color color) { }
	public virtual void SetCUEKeyAnimationColor(CUE.NET.Devices.Generic.Enums.CorsairLedId key, Color color) { }
	public virtual Color GetChromaKeyAnimationColor(ChromaSDK.Keyboard.RZKEY key, Color color) { return Color.black; }
	public virtual Color GetCUEKeyAnimationColor(CUE.NET.Devices.Generic.Enums.CorsairLedId key, Color color) { return Color.black; }
	#endregion
}
