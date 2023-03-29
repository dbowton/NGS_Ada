using UnityEngine;

public class EmptyController : RGBController
{
	public override bool Init() { return true; }
	public override void Update() { }
	public override void Shutdown() { }
	public override void ClearAnimationButtons() { }
	public override void ClearAnimationKeys() { }
	public override void ClearButtons() { }
	public override void ClearKeys() { }
	public override Color GetButtonAnimationColor() { return Color.black; }
	public override Color GetButtonColor() { return Color.black; }
	public override Color GetKeyAnimationColor(KeyCode keyCode) { return Color.black; }
	public override Color GetKeyColor(KeyCode keyCode) { return Color.black; }
	public override void SetButtonAnimationColor(Color color) { }
	public override void SetButtonColor(Color color) { }
	public override void SetKeyAnimationColor(KeyCode keyCode, Color color) { }
	public override void SetKeyColor(KeyCode keyCode, Color color) { }
}
