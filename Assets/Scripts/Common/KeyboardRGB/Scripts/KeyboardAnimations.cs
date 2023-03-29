using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUE.NET.Devices.Generic.Enums;

public static class KeyboardAnimations
{
	private static Timer animationTimer = null;
	private static int animationIndex = 0;
	private static List<System.Action> animation = new List<System.Action>();

	private static RGBController controller = null;
	private static Color color = Color.red;

	public class Animation
	{
		public Animation(float delay, List<System.Action> actions)
		{
			this.delay = delay;
			this.actions = actions;
		}

		public float delay = 0.5f;
		public List<System.Action> actions = new List<System.Action>();
	}

	public static void StopAnimation()
	{
		Reset();
	}

	private static void Reset()
	{
		if (controller != null)
			controller.ClearAnimationColors();

		if (animationTimer != null)
			Clock.instance.RemoveTimer(animationTimer);

		animationIndex = 0;
		animationTimer = null;
	}
	public static void PlayAnimation(RGBController controller, Animation animation, Color color)
	{
		KeyboardAnimations.controller = controller;
		KeyboardAnimations.animation = animation.actions;
		KeyboardAnimations.color = color;
		Reset();

		animationTimer = new Timer(animation.delay, () =>
		{
			KeyboardAnimations.animation[animationIndex++].Invoke();
			if (animationIndex == KeyboardAnimations.animation.Count) Reset();
		}, false, true);
	}

	public static Animation Random
	{
		get
		{
			return new Animation(0.34f,
				new List<System.Action>()
				{
					() =>
					{
						controller.ClearAnimationColors();
						List<KeyCode> keys = new List<KeyCode>();

						while(keys.Count < 16)
						{
							KeyCode key = (KeyCode)Enum.GetValues(typeof(KeyCode)).GetValue(UnityEngine.Random.Range(0, Enum.GetValues(typeof(KeyCode)).Length));
							if(KeyConverter.KeycodeToRZKey(key).Equals(ChromaSDK.Keyboard.RZKEY.RZKEY_INVALID) || 
									KeyConverter.KeycodeToCorsairLedId(key).Equals(CorsairLedId.Invalid) || 
									keys.Contains(key)) continue;

							keys.Add(key);
						}

						foreach(var key in keys)
						{
							controller.SetKeyAnimationColor(key,
								new Color(UnityEngine.Random.Range(0f, 1),
										UnityEngine.Random.Range(0f, 1),
										UnityEngine.Random.Range(0f, 1)));
						}

						--animationIndex;
					}
				});
		}
	}
	public static Animation Burst
	{
		get
		{
			return new Animation(0.125f,
				new List<System.Action>()
				{
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.G, color);
						controller.SetKeyAnimationColor(KeyCode.H, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.T, color);
						controller.SetKeyAnimationColor(KeyCode.F, color);
						controller.SetKeyAnimationColor(KeyCode.V, color);
						controller.SetKeyAnimationColor(KeyCode.B, color);
						controller.SetKeyAnimationColor(KeyCode.N, color);
						controller.SetKeyAnimationColor(KeyCode.J, color);
						controller.SetKeyAnimationColor(KeyCode.U, color);
						controller.SetKeyAnimationColor(KeyCode.Y, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Alpha5, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha6, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha7, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha8, color);
						controller.SetKeyAnimationColor(KeyCode.R, color);
						controller.SetKeyAnimationColor(KeyCode.D, color);
						controller.SetKeyAnimationColor(KeyCode.C, color);
						controller.SetKeyAnimationColor(KeyCode.I, color);
						controller.SetKeyAnimationColor(KeyCode.K, color);
						controller.SetKeyAnimationColor(KeyCode.M, color);
						controller.SetKeyAnimationColor(KeyCode.Space, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Alpha4, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha9, color);
						controller.SetKeyAnimationColor(KeyCode.E, color);
						controller.SetKeyAnimationColor(KeyCode.S, color);
						controller.SetKeyAnimationColor(KeyCode.X, color);
						controller.SetKeyAnimationColor(KeyCode.O, color);
						controller.SetKeyAnimationColor(KeyCode.L, color);
						controller.SetKeyAnimationColor(KeyCode.Comma, color);
						controller.SetKeyAnimationColor(KeyCode.Space, color);
					},	
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Alpha3, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha0, color);
						controller.SetKeyAnimationColor(KeyCode.W, color);
						controller.SetKeyAnimationColor(KeyCode.A, color);
						controller.SetKeyAnimationColor(KeyCode.Z, color);
						controller.SetKeyAnimationColor(KeyCode.LeftAlt, color);
						controller.SetKeyAnimationColor(KeyCode.RightAlt, color);
						controller.SetKeyAnimationColor(KeyCode.P, color);
						controller.SetKeyAnimationColor(KeyCode.Colon, color);
						controller.SetKeyAnimationColor(KeyCode.Period, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
					}
				});
		}
	}
	public static Animation Drop
	{
		get
		{
			return new Animation(0.125f,
				new List<System.Action>()
				{
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Escape, color);
						controller.SetKeyAnimationColor(KeyCode.F1, color);
						controller.SetKeyAnimationColor(KeyCode.F2, color);
						controller.SetKeyAnimationColor(KeyCode.F3, color);
						controller.SetKeyAnimationColor(KeyCode.F4, color);
						controller.SetKeyAnimationColor(KeyCode.F5, color);
						controller.SetKeyAnimationColor(KeyCode.F6, color);
						controller.SetKeyAnimationColor(KeyCode.F7, color);
						controller.SetKeyAnimationColor(KeyCode.F8, color);
						controller.SetKeyAnimationColor(KeyCode.F9, color);
						controller.SetKeyAnimationColor(KeyCode.F10, color);
						controller.SetKeyAnimationColor(KeyCode.F11, color);
						controller.SetKeyAnimationColor(KeyCode.F12, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Tilde, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha1, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha2, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha3, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha4, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha5, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha6, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha7, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha8, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha9, color);
						controller.SetKeyAnimationColor(KeyCode.Alpha0, color);
						controller.SetKeyAnimationColor(KeyCode.Underscore, color);
						controller.SetKeyAnimationColor(KeyCode.Equals, color);
						controller.SetKeyAnimationColor(KeyCode.Backspace, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Tab, color);
						controller.SetKeyAnimationColor(KeyCode.Q, color);
						controller.SetKeyAnimationColor(KeyCode.W, color);
						controller.SetKeyAnimationColor(KeyCode.E, color);
						controller.SetKeyAnimationColor(KeyCode.R, color);
						controller.SetKeyAnimationColor(KeyCode.T, color);
						controller.SetKeyAnimationColor(KeyCode.Y, color);
						controller.SetKeyAnimationColor(KeyCode.U, color);
						controller.SetKeyAnimationColor(KeyCode.I, color);
						controller.SetKeyAnimationColor(KeyCode.O, color);
						controller.SetKeyAnimationColor(KeyCode.P, color);
						controller.SetKeyAnimationColor(KeyCode.LeftBracket, color);
						controller.SetKeyAnimationColor(KeyCode.RightBracket, color);
						controller.SetKeyAnimationColor(KeyCode.Backslash, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.CapsLock, color);
						controller.SetKeyAnimationColor(KeyCode.A, color);
						controller.SetKeyAnimationColor(KeyCode.S, color);
						controller.SetKeyAnimationColor(KeyCode.D, color);
						controller.SetKeyAnimationColor(KeyCode.F, color);
						controller.SetKeyAnimationColor(KeyCode.G, color);
						controller.SetKeyAnimationColor(KeyCode.H, color);
						controller.SetKeyAnimationColor(KeyCode.J, color);
						controller.SetKeyAnimationColor(KeyCode.K, color);
						controller.SetKeyAnimationColor(KeyCode.L, color);
						controller.SetKeyAnimationColor(KeyCode.Colon, color);
						controller.SetKeyAnimationColor(KeyCode.Quote, color);
						controller.SetKeyAnimationColor(KeyCode.Return, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.LeftShift, color);
						controller.SetKeyAnimationColor(KeyCode.Z, color);
						controller.SetKeyAnimationColor(KeyCode.X, color);
						controller.SetKeyAnimationColor(KeyCode.C, color);
						controller.SetKeyAnimationColor(KeyCode.V, color);
						controller.SetKeyAnimationColor(KeyCode.B, color);
						controller.SetKeyAnimationColor(KeyCode.N, color);
						controller.SetKeyAnimationColor(KeyCode.M, color);
						controller.SetKeyAnimationColor(KeyCode.Comma, color);
						controller.SetKeyAnimationColor(KeyCode.Period, color);
						controller.SetKeyAnimationColor(KeyCode.Slash, color);
						controller.SetKeyAnimationColor(KeyCode.RightShift, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.LeftControl, color);
						controller.SetKeyAnimationColor(KeyCode.LeftWindows, color);
						controller.SetKeyAnimationColor(KeyCode.LeftAlt, color);
						controller.SetKeyAnimationColor(KeyCode.Space, color);
						controller.SetKeyAnimationColor(KeyCode.RightAlt, color);

						controller.SetChromaKeyAnimationColor(ChromaSDK.Keyboard.RZKEY.RZKEY_FN, color);
						controller.SetCUEKeyAnimationColor(CorsairLedId.Fn, color);

						controller.SetKeyAnimationColor(KeyCode.Menu, color);
						controller.SetKeyAnimationColor(KeyCode.RightControl, color);
					},
					() =>
					{
						controller.ClearAnimationColors();
					}
				});
		}
	}
	public static Animation Wave
	{
		get
		{
			return new Animation(0.125f,
			new List<System.Action>()
			{
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Q, color);
						controller.SetKeyAnimationColor(KeyCode.A, color);
						controller.SetKeyAnimationColor(KeyCode.Z, color);
						controller.SetKeyAnimationColor(KeyCode.RightBracket, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.Quote, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.Question, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.W, color);
						controller.SetKeyAnimationColor(KeyCode.S, color);
						controller.SetKeyAnimationColor(KeyCode.X, color);
						controller.SetKeyAnimationColor(KeyCode.LeftBracket, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.Colon, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.Greater, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.E, color);
						controller.SetKeyAnimationColor(KeyCode.D, color);
						controller.SetKeyAnimationColor(KeyCode.C, color);
						controller.SetKeyAnimationColor(KeyCode.P, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.L, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.Less, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.R, color);
						controller.SetKeyAnimationColor(KeyCode.F, color);
						controller.SetKeyAnimationColor(KeyCode.V, color);
						controller.SetKeyAnimationColor(KeyCode.O, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.K, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.M, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.T, color);
						controller.SetKeyAnimationColor(KeyCode.G, color);
						controller.SetKeyAnimationColor(KeyCode.B, color);
						controller.SetKeyAnimationColor(KeyCode.I, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.J, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.N, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Y, color);
						controller.SetKeyAnimationColor(KeyCode.H, color + Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.B, color + Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.N, color + Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.U, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.Y, Color.white);
						controller.SetKeyAnimationColor(KeyCode.H, Color.white);
						controller.SetKeyAnimationColor(KeyCode.B, Color.white);
						controller.SetKeyAnimationColor(KeyCode.N, Color.white);
						controller.SetKeyAnimationColor(KeyCode.U, Color.white);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.U, color);
						controller.SetKeyAnimationColor(KeyCode.J, color);
						controller.SetKeyAnimationColor(KeyCode.M, color);
						controller.SetKeyAnimationColor(KeyCode.Y, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.G, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.V, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.I, color);
						controller.SetKeyAnimationColor(KeyCode.K, color);
						controller.SetKeyAnimationColor(KeyCode.Comma, color);
						controller.SetKeyAnimationColor(KeyCode.T, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.F, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.C, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.O, color);
						controller.SetKeyAnimationColor(KeyCode.L, color);
						controller.SetKeyAnimationColor(KeyCode.Period, color);
						controller.SetKeyAnimationColor(KeyCode.R, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.D, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.X, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.P, color);
						controller.SetKeyAnimationColor(KeyCode.Colon, color);
						controller.SetKeyAnimationColor(KeyCode.Slash, color);
						controller.SetKeyAnimationColor(KeyCode.E, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.S, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.Z, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.LeftBracket, color);
						controller.SetKeyAnimationColor(KeyCode.Quote, color);
						controller.SetKeyAnimationColor(KeyCode.W, Color.white - color);
						controller.SetKeyAnimationColor(KeyCode.A, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
						controller.SetKeyAnimationColor(KeyCode.RightBracket, color);
						controller.SetKeyAnimationColor(KeyCode.Q, Color.white - color);
					},
				() =>
					{
						controller.ClearAnimationColors();
					}
			});
		}
	}
}
