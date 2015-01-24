using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;

public class Leak : IObj
{
	[Inject]
	public AddWater addWater { get; set; }

	[Inject]
	public SpringLeak springLeak { get; set; }

	[Inject]
	public FixLeak fixLeak { get; set; }

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

	private void Enable()
	{
		gameObject.SetActive (true);
	}

	private void Disable()
	{
		gameObject.SetActive (false);
	}

	void Start()
	{
		springLeak.AddListener (SpringLeak);
		Plugged = false;
		Disable ();
	}

	void Update()
	{
		if(!Plugged)
		{
			addWater.Dispatch(waterPerSecond * Time.deltaTime);
		}
	}

	public void SpringLeak(Leak leak)
	{
		if(leak.GetHashCode() == GetHashCode())
		{
			Enable ();
		}
	}

	public void FixLeak()
	{
		fixLeak.Dispatch (this);
		Disable ();
	}

	public override void ADown ()
	{
		Plugged = true;
	}
	
	public override void AUp ()
	{
		Plugged = false;
	}
	
	public override void BDown ()
	{
		FixLeak ();
	}
	
	public override void BUp ()
	{
	}
}
