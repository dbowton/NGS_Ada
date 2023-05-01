using System;
using UnityEngine;

public class Timer
{
	private float timer = 0.0f;
	private float time = 0.0f;

	private bool paused = false;
	private bool autoReset = false;
	private bool oneTime = false;
	private bool useScaledTime = false;

	public bool IsOver { get { return timer <= 0; } }
	public float GetElapsed { get { if (time <= 0) return -1; return 1 - (timer / time); } }

	private Action function;

	public Timer(bool oneTime = false)
	{
		this.time = 0;
		this.timer = time;
		this.oneTime = oneTime;

		if (Clock.instance == null)
			new GameObject().AddComponent<Clock>();

		Clock.instance.AddTimer(this);
	}

	public Timer(Action function, bool oneTime = false)
	{
		this.time = 0;
		this.timer = time;
		this.function = function;
		this.oneTime = oneTime;

		if (Clock.instance == null)
			new GameObject().AddComponent<Clock>();

		Clock.instance.AddTimer(this);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="time">how long to run the timer</param>
	/// <param name="function">funciton to call when timer ends</param>
	/// <param name="oneTime">whether or not to destroy timer when it ends</param>
	/// <param name="autoReset">whether or not the timer will restart itself</param>
	public Timer(float time, Action function = null, bool oneTime = false, bool autoReset = false, bool useScaledTime = true)
	{
		this.time = time;
		this.timer = time;
		this.function = function;

		this.oneTime = oneTime;
		this.autoReset = autoReset;

		if (time <= 0) function?.Invoke();

		if (Clock.instance == null)
			new GameObject().AddComponent<Clock>();

		this.useScaledTime = useScaledTime;
		Clock.instance.AddTimer(this);
	}

	public void Update()
	{
		if (paused) return;

		if (timer > 0)
		{
			timer -= (useScaledTime) ? Time.deltaTime : Time.unscaledDeltaTime;

			if (timer <= 0)
			{
				function?.Invoke();
				if(oneTime)
					Clock.instance.RemoveTimer(this);
				else if (autoReset) 
					timer = time;
			}
		}
	}

	public void End()
	{
		timer = 0;
	}

	public void Modify(float time)
	{
		Modify(time, function);
	}
	public void Modify(float time, Action function)
	{
		Modify(time, function, autoReset);
	}
	public void Modify(float time, Action function, bool autoReset)
	{
		this.time = time;
		this.function = function;
		this.autoReset = autoReset;
	}

	/// <summary>
	/// used to add or remove time from the timer
	/// </summary>
	public void Manipulate(float change)
	{
		timer += change;
	}

	/// <summary>
	/// used to directly set the timer
	/// </summary>
	public void Set(float setTime)
	{
		timer = setTime;
	}


	public void Reset()
	{
		timer = time;
	}

	public void Pause()
	{
		paused = true;
	}

	public void Resume()
	{
		paused = false;
	}

	/// <summary>
	/// Removes the timer from updates
	/// </summary>
	public void Remove()
	{
		Clock.instance.RemoveTimer(this);
	}

	/// <summary>
	/// returns the time
	/// </summary>
	public float GetTime()
	{
		return time;
	}
}
