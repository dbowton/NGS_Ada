using UnityEngine;

public class TestTimer : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text text;
    [SerializeField] float time = 4f;

	Stopwatch stopwatch;

	private void Start()
	{
		Timer printTimer = new Timer(time, () => { print("Hello World"); }, false, true);
		stopwatch = new Stopwatch();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			stopwatch.Lap();
		if (Input.GetKeyDown(KeyCode.F))
			stopwatch.Stop();
		if(Input.GetKeyDown(KeyCode.E))
			stopwatch.Resume();
		if (Input.GetKeyDown(KeyCode.R))
			stopwatch.Reset();

		text.text = stopwatch.ToString();
	}
}
