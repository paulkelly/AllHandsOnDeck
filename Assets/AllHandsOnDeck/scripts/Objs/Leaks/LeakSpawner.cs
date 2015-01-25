using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using AllHandsOnDeck.Common;

public class LeakSpawner : View
{
	[Inject]
	public SpringLeak springLeak { get; set; }
	
	[Inject]
	public FixLeak fixLeak { get; set; }
	
	[Inject]
	public StartGame startGame { get; set; }
	
	private bool started = false;

	public List<Leak> leaks = new List<Leak>();

	public float startDelay = 3;

	[Range (1,8)]
	public float minSpawnTime;

	[Range (2,16)]
	public float maxSpawnTime;

	private float timeMulti = 120;
	private float multiTimer = 0;
	private float timeMultiplier;

	private float timer;
	private float maxTime = 1;

	void Update()
	{
		if(!started)
		{	
			return;
		}
	
		multiTimer += Time.deltaTime;
	
		if(timer < maxTime)
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0;
			timeMultiplier = Mathf.Max(0.7f, 1 - (multiTimer / timeMulti));
			maxTime = Random.Range(minSpawnTime, maxSpawnTime) * timeMultiplier;

			Spawn ();
		}
	}

	void Start()
	{
		startGame.AddListener(StartTheGame);
		fixLeak.AddListener (FixedLeak);
		timeMultiplier = Mathf.Max(0.7f, 1 - (timeMulti * multiTimer));
		maxTime = Random.Range(minSpawnTime, maxSpawnTime) + startDelay;
	}
	
	private void StartTheGame()
	{
		started = true;
	}

	public void Spawn()
	{
		if(leaks.Count > 0)
		{
			int index = Random.Range (0, leaks.Count);

			springLeak.Dispatch (leaks [index]);
			leaks.RemoveAt (index);
		}
	}

	public void FixedLeak(Leak leak)
	{
		leaks.Add (leak);
	}
}
