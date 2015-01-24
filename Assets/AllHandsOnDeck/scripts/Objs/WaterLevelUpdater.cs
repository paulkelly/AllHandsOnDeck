using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class WaterLevelUpdater : View
{
	[Inject]
	public AddWater addWater { get; set; }

	private float waterLevel = 0;
	public Text levelGui;

	protected override void OnStart()
	{
		addWater.AddListener (IncreaseWaterLevel);
	}

	void Update()
	{
		levelGui.text = "" + Mathf.FloorToInt(waterLevel);
	}

	private void IncreaseWaterLevel(float amount)
	{
		Debug.Log ("Adding water: " + amount);
		waterLevel += amount;
	}

}
