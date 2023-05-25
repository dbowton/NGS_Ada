using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public int bit;

	[SerializeField] int maxBit;
	[SerializeField] AudioClip collectedSound;
	[SerializeField] AudioClip completedSound;
	int bitMap = 0;

	private void Start()
	{
		if (PlayerPrefs.HasKey("lookedAt")) bitMap = PlayerPrefs.GetInt("lookedAt");
		if (PlayerPrefs.HasKey("hell") && PlayerPrefs.GetInt("hell") == 1) Destroy(this);
	}

	private void Save()
	{
		if(bitMap != (bitMap | (1 << bit))) AudioSource.PlayClipAtPoint(collectedSound, transform.position);

		bitMap |= (1 << bit);

		for (int i = 0; i <= maxBit; i++)
		{
			if ((bitMap & (1 << i)) == 0) return;
		}

		AudioSource.PlayClipAtPoint(completedSound, transform.position);

		PlayerPrefs.SetInt("hell", 1);
		Destroy(this);


	}

	private void OnDestroy()
	{
		PlayerPrefs.SetInt("lookedAt", bitMap);
	}
}
