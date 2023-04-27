using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock instance = null;
	List<Timer> timers = new List<Timer>();
	List<Stopwatch> stopwatches = new List<Stopwatch>();

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			gameObject.name = "Clock";
		}
		else
			Destroy(this);
	}

	void Update()
    {
		for(int i = 0; i < timers.Count; ) 
		{
			if (timers[i] != null)
			{
				timers[i].Update(Time.unscaledDeltaTime);
				i++;
			}
			else
			{
				RemoveTimer(timers[i]);
			}
		}

		for(int i = 0; i < stopwatches.Count; i++) 
		{
			stopwatches[i].Update(Time.unscaledDeltaTime);
		}
    }

	public void AddTimer(Timer timer)
	{
		if(!timers.Contains(timer))
			timers.Add(timer);
	}

	public void AddStopwatch(Stopwatch stopwatch)
	{
		if (!stopwatches.Contains(stopwatch))
			stopwatches.Add(stopwatch);
	}

	public void RemoveTimer(Timer timer) 
	{
		if (timers.Contains(timer))
			timers.Remove(timer);
	}

	public void RemoveStopwatch(Stopwatch stopwatch)
	{
		if(stopwatches.Contains(stopwatch))
			stopwatches.Remove(stopwatch);
	}
}
