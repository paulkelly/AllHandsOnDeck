using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;

public class Leak : IObj
{
	[Inject]
	public AddWater addWater { get; set; }

	private bool plugged = false;
	public bool Plugged
	{
		get
		{
			return plugged;
		}

		set
		{
			plugged = value;
			unpluggedEffect.Enabled = plugged;
			pluggedEffect.Enabled = !plugged;
		}
	}
	public PartEffectEnabler unpluggedEffect;
	public PartEffectEnabler pluggedEffect;

	[Range (0,60)]
	public float waterPerSecond;

	void Start()
	{
		Plugged = false;
	}

	void Update()
	{
		if(!Plugged)
		{
			addWater.Dispatch(waterPerSecond * Time.deltaTime);
		}
	}

	public override void ADown ()
	{
		Debug.Log ("ADown Leak");
		Plugged = true;
	}
	
	public override void AUp ()
	{
		Debug.Log ("AUp Leak");
		Plugged = false;
	}
	
	public override void BDown ()
	{
	}
	
	public override void BUp ()
	{
	}
}
