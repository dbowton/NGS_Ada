using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    public Cow cowScript;
    public Slider ChewAmountSlider;
    public Slider TailSwipeAmountSlider;
    public Slider TurnAmountSlider;

    /// <summary>
    /// 
    /// </summary>
    public void MooButton_Click()
    {
        cowScript.mooTrigger = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void WalkButton_Click()
    {
        cowScript.walking = !cowScript.walking;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChewAmountSlider_ValueChanged()
    {
        cowScript.ChewAmount = ChewAmountSlider.value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void TailSwipeAmountSlider_ValueChanged()
    {
        cowScript.TailSwipeAmount = TailSwipeAmountSlider.value;
    }

    /// <summary>
    /// 
    /// </summary>
    public void TurnAmountSlider_ValueChanged()
    {
        cowScript.TurnAmount = TurnAmountSlider.value;
    }
}
