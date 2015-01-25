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

	[Inject]
	public EndGame endgame { get; set; }

	public AudioSource burst;
	public AudioSource constant;
	public AudioSource plugedSFX;

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
			if(!plugged)
			{
				burst.Play ();
			}
			unpluggedEffect.Enabled = !plugged;
			pluggedEffect.Enabled = plugged;
		}
	}
	public PartEffectEnabler unpluggedEffect;
	public PartEffectEnabler pluggedEffect;

	[Range (0,60)]
	public float waterPerSecond;

	private void Enable()
	{
		gameObject.SetActive (true);
		burst.Play ();
		constant.Play ();

		if(plug != null)
		{
			plug.Pop();
		}
	}

	private void Disable()
	{
		gameObject.SetActive (false);
		burst.Stop ();
		constant.Stop ();
		plugedSFX.Play ();
	}

	void Start()
	{
		endgame.AddListener (EndTheGame);
		springLeak.AddListener (SpringLeak);
		Plugged = false;
		Disable ();
	}

	private void EndTheGame()
	{
		gameOver = true;
	}

	bool gameOver = false;

	private float sfxStartVol = 1;
	private float sfxEndVol = 0;
	
	private float sfxFadeTime = 0;
	private float maxSfxFadeTime = 4f;
	void Update()
	{
		if(!Plugged)
		{
			addWater.Dispatch(waterPerSecond * Time.deltaTime);
		}

		if(gameOver)
		{
			sfxFadeTime += Time.deltaTime;
			float percComplete = sfxFadeTime / maxSfxFadeTime;
			if(percComplete < 1)
			{
				constant.volume = sfxStartVol + ((sfxEndVol - sfxStartVol) * percComplete);
			}
			else
			{
				constant.volume = sfxEndVol;
			}
		}
	}

	public void SpringLeak(Leak leak)
	{
		if(gameOver)
		{
			return;
		}

		if(leak.GetHashCode() == GetHashCode())
		{
			Enable ();
		}
	}

	private Plug plug = null;
	public override void FixLeak(Plug plug)
	{
		this.plug = plug;
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
		//FixLeak ();
	}
	
	public override void BUp ()
	{
	}
}
