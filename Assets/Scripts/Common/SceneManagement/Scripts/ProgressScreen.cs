using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScreen : MonoBehaviour
{
	private float progress = 0f;
	[SerializeField] Slider slider;


	public float Progress
	{
		private get { return progress; }
		set 
		{ 
			progress = value; 
			slider.value = progress;
		}
	}
}
