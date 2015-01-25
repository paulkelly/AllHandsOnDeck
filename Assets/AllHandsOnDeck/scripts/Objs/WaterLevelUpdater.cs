using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class WaterLevelUpdater : View
{
	[Inject]
	public AddWater addWater { get; set; }

	[Inject]
	public RemoveWater removeWater { get; set; }
	
	[Inject]
	public EndGame endgame { get; set; }
	
	[Inject]
	public RollCredits rollCredits { get; set; }
	
	[Inject]
	public StartGame startGame { get; set; }

	public AudioSource endGame;
	public AudioSource music;
	public AudioSource waterSFX;

	private bool started = false;
	private bool ended = false;
	private bool rolledCredits = false;
	private float endCinTimer = 0;
	private float endCinTime = 0.5f;

	public Transform ship;
	public Transform water;
	public float minY;
	public float maxY;

	private float waterLevel = 0;
	public float waterMinY;
	public float waterMaxY;
	public Text levelGui;
	
	private float surviveTime = 0;

	protected override void OnStart()
	{
		addWater.AddListener (IncreaseWaterLevel);
		removeWater.AddListener (DecreaseWaterLevel);
		startGame.AddListener(StartTheGame);
	}
	
	private void StartTheGame()
	{
		started = true;
	}

	private float musicStartVol = 1;
	private float musicEndVol = 0;
	private float waterStartVol = 0.35f;
	private float waterEndVol = 0.1f;

	private float musicFadeTime = 0;
	private float maxMusicFadeTime = 2f;

	void Update()
	{
		if(!started)
		{
			return;
		}
		
		if(!ended)
		{
			surviveTime += Time.deltaTime;
		}
		else if(!rolledCredits && endCinTimer > endCinTime)
		{
			rolledCredits = true;
			endGame.Play();
			rollCredits.Dispatch(surviveTime);
		}
		else
		{
			endCinTimer += Time.deltaTime;
		}

		if(rolledCredits)
		{
			musicFadeTime += Time.deltaTime;
			float percComplete = musicFadeTime / maxMusicFadeTime;
			if(percComplete < 1)
			{
				music.volume = musicStartVol + ((musicEndVol - musicStartVol) * percComplete);
				waterSFX.volume = waterStartVol + ((waterEndVol - waterStartVol) * percComplete);
			}
			else
			{
				music.volume = 0;
				waterSFX.volume = waterEndVol;
			}
		}
	
		//levelGui.text = "" + Mathf.FloorToInt(waterLevel);
		water.renderer.material.color = new Color (water.renderer.material.color.r, water.renderer.material.color.g, water.renderer.material.color.b, waterLevel / 100);

		float y = maxY - (maxY - (waterLevel / 100) * minY);
		float dist = ship.position.y - y;
		dist = Mathf.Min (Time.deltaTime / 5, Mathf.Abs(dist)) * Mathf.Sign (dist);
		ship.position = new Vector3 (ship.position.x, ship.position.y - dist, ship.position.z);

		float waterY = waterMinY + ((waterLevel / 100) * (waterMaxY - waterMinY));
		water.localPosition = new Vector3 (water.localPosition.x, waterY, water.localPosition.z);
	}

	private void IncreaseWaterLevel(float amount)
	{
		waterLevel += amount;
		
		if(waterLevel > 100)
		{
			endgame.Dispatch();
			ended = true;
		}
	}

	private void DecreaseWaterLevel(float amount)
	{
		waterLevel -= amount;
		if(waterLevel < 0)
		{
			waterLevel = 0;
		}
	}

}
