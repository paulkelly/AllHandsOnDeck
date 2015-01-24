using strange.extensions.mediation.impl;
using UnityEngine;

public class IObj : View
{
	public virtual void PickUp (Transform chrItem)
	{
	}

	public virtual void Drop ()
	{
	}

	public virtual void ThrowWater(bool outside)
	{
	}

	public virtual void FixLeak(Plug plug)
	{
	}

	public virtual void Use(Leak leak)
	{
	}

	public virtual void ADown ()
	{
	}

	public virtual void AUp ()
	{
	}

	public virtual void BDown ()
	{
	}
	
	public virtual void BUp ()
	{
	}
}
