using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class TheSimpletonScript : MonoBehaviour
{
	public KMBombModule Module;
	public KMAudio Audio;
	public KMBombInfo Info;
	
	public TextMesh Victory;
	public AudioClip SFX;
	
	public KMSelectable Button;
	private int Once = 0;

	//Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool ModuleSolved;
	
	void Awake()
	{
		moduleId = moduleIdCounter++;
		Button.OnInteract += delegate () {Pressed(); return false; };
		Button.OnInteractEnded += delegate () {Unpressed();};
		Debug.LogFormat("[The Simpleton #{0}] Push the button!", moduleId);
	}
	
	void Pressed()
	{
		if (Once != 2)
		{
			Once = 1;
		}
		Button.AddInteractionPunch(0.2f);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
	}
	
	void Unpressed()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonRelease, transform);
		if (Once == 1)
		{
			Audio.PlaySoundAtTransform(SFX.name, transform);
			Module.HandlePass();
			Once = 2;
			Victory.text = "VICTORY";
			Debug.LogFormat("[The Simpleton #{0}] You pushed the button. Good job!", moduleId);
		}
	}
}