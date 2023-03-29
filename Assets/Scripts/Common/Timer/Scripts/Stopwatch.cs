using System.Collections.Generic;
using UnityEngine;

public class Stopwatch
{
	List<float> laps = new List<float>();
	float lapTime;
	float currentTime;

	bool running = true;


	public Stopwatch()
	{
		if (Clock.instance == null)
			new GameObject().AddComponent<Clock>();

		Clock.instance.AddStopwatch(this);
	}


	public void Update(float time)
	{
		if (running)
			currentTime += time;
	}

	public int GetLapCount()
	{
		return laps.Count;
	}

	public void Lap()
	{

		laps.Add(currentTime - lapTime);
		lapTime = currentTime;
	}

	public float GetLap(int lapNumber)
	{
		return laps[lapNumber];
	}

	public float GetDifference(int lap1Number, int lap2Number) 
	{ 
		return GetLap(lap2Number) - GetLap(lap1Number);
	}

	public float GetTotal()
	{
		return currentTime;
	}

	public void Stop()
	{
		running = false;
	}

	public void Resume()
	{
		running = true;
	}

	public void Reset()
	{
		laps.Clear();
		currentTime = 0;
	}

	public void Remove()
	{
		Clock.instance.RemoveStopwatch(this);
	}

	public override string ToString()
	{
		string workingStr = "";
		if (this.GetLapCount() > 0)
		{
			workingStr += "lap 1: " + this.GetLap(0).ToString("f2") + "s\n";
			for (int i = 1; i < this.GetLapCount(); i++)
			{
				workingStr += "lap " + (i + 1) + ": " + this.GetLap(i).ToString("f2") + "s : ";
				float difference = this.GetDifference(i - 1, i);
				workingStr += (difference > 0) ? "+" : "";

				workingStr += this.GetDifference(i - 1, i).ToString("f2") + "\n";
			}
		}
		workingStr += this.GetTotal().ToString("f2") + "s";

		return workingStr;
	}
}
