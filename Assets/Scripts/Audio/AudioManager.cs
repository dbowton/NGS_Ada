using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup;
    public List<Sound> sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else
		{
            Destroy(gameObject); 
            return;
        }
            

        DontDestroyOnLoad(gameObject);

        foreach (var sound in sounds)
		{
            sound.source = gameObject.AddComponent<AudioSource>();
 
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
		}
    }

	void Start()
	{
        Play("Theme");
	}

	public void Play(string name)
	{
        Sound sound = sounds.Find(sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        sound.source.Play();
	}

    public void Stop(string name)
	{
        Sound sound = sounds.Find(sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        sound.source.Stop();
    }
}
