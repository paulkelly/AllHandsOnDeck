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
	public Transform water;
	public float minY;
	public float maxY;

	private float waterLevel = 0;
	public float waterMinY;
	public float waterMaxY;
	public Text levelGui;

	protected override void OnStart()
	{
		addWater.AddListener (IncreaseWaterLevel);
		removeWater.AddListener (DecreaseWaterLevel);
	}

	void Update()
	{
		levelGui.text = "" + Mathf.FloorToInt(waterLevel);
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
