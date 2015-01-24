using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartEffectEnabler : MonoBehaviour
{
	public List<ParticleSystem> particeSystems;

	public bool Enabled
	{
		set
		{
			if(value)
			{
				Enable();
			}
			else
			{
				Disable();
			}
		}
	}

	public void Enable()
	{
		foreach(ParticleSystem system in particeSystems)
		{
			system.Stop();
		}
	}

	public void Disable()
	{
		foreach(ParticleSystem system in particeSystems)
		{
			system.Play();
		}
	}
}
