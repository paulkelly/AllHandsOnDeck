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

	public Transform ship;
	public float minY;
	public float maxY;

	private float waterLevel = 0;
	public Text levelGui;

	protected override void OnStart()
	{
		addWater.AddListener (IncreaseWaterLevel);
		removeWater.AddListener (DecreaseWaterLevel);
	}

	void Update()
	{
		levelGui.text = "" + Mathf.FloorToInt(waterLevel);
		float y = maxY - (maxY - (waterLevel / 100) * minY);
		ship.position = new Vector3 (ship.position.x, y, ship.position.z);
	}

	private void IncreaseWaterLevel(float amount)
	{
		waterLevel += amount;
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
