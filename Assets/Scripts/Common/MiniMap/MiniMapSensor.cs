using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapSensor : MonoBehaviour
{
    [SerializeField] float range;
	[SerializeField] List<SensorDetection> detections = new List<SensorDetection>();

	[SerializeField] float UISizeAdjustment = 0.15f;
	[SerializeField] float UILimits = 2f;

    [SerializeField] GameObject UI;
    private List<GameObject> detectedObjects = new List<GameObject>();

	private void Start()
	{
		for (int i = 0; i < detections.Count;)
		{
			Type type = Type.GetType(detections[i].objectType);

			if (type == null)
			{
				print(detections[i].objectType + " Not Found");
				detections.RemoveAt(i);
				continue;
			}

			detections[i].searchType = type;
			i++;
		}
	}

	[System.Serializable]
    public class SensorDetection
    {
		public string objectType = "";
		[HideInInspector] public Type searchType;

        public Sprite default_sprite;
    }

    void Detect()
    {
		foreach (var go in detectedObjects)
			Destroy(go);

		detectedObjects.Clear();

		var collisions = Physics.OverlapBox(transform.position, Vector3.one * range);
		Vector2 UISizeDelta = UI.GetComponent<RectTransform>().sizeDelta;

		foreach (var collision in collisions)
		{
			foreach (var recordedType in detections)
			{
				if(collision.TryGetComponent(recordedType.searchType, out Component foundType) || collision.transform.root.TryGetComponent(recordedType.searchType, out foundType))
				{
					//	Create Blip
					GameObject NewObj = new GameObject();
					NewObj.name = "detected_" + collision.name;
					Image img = NewObj.AddComponent<Image>();

					NewObj.GetComponent<RectTransform>().sizeDelta = UISizeDelta * UISizeAdjustment;
					NewObj.GetComponent<RectTransform>().SetParent(UI.transform, false);

					// Assign Sprite
					img.sprite = recordedType.default_sprite;

					// Set Position
					Vector3 pos = collision.transform.position - transform.position;
					Vector2 setPos = new Vector2(pos.x, pos.z);

					setPos /= range * UILimits;
					setPos.x *= UISizeDelta.x;
					setPos.y *= UISizeDelta.y;

					NewObj.GetComponent<RectTransform>().anchoredPosition = setPos;

					// Add Blip
					detectedObjects.Add(NewObj);
					break;
				}
			}
		}
	}

    void Update()
    {
		Detect();
	}
}
