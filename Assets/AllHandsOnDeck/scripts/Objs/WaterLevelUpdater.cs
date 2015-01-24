using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class WaterLevelUpdater : View
{
	[Inject]
	public AddWater addWater { get; set; }

	public Transform ship;
	public float minY;
	public float maxY;

	private float waterLevel = 0;
	public Text levelGui;

	protected override void OnStart()
	{
		addWater.AddListener (IncreaseWaterLevel);
	}

	void Update()
	{
		levelGui.text = "" + Mathf.FloorToInt(waterLevel);
		float y = maxY - (maxY - (waterLevel / 100) * minY);
		ship.position = new Vector3 (ship.position.x, y, ship.position.z);
	}

	private void IncreaseWaterLevel(float amount)
	{
		Debug.Log ("Adding water: " + amount);
		waterLevel += amount;
	}

}
