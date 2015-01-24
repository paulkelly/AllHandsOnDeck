using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;

public class Bucket : IObj
{
	[Inject]
	public RemoveWater removeWater { get; set; }

	[Inject]
	public RemoveObject removeObject { get; set; }

	public Collider interactCollider;

	public Transform world;

	[Range (0,6)]
	public float volume;

	private bool full = false;

	public override void PickUp(Transform chrItem)
	{
		transform.parent = chrItem;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		rigidbody.isKinematic = true;
		removeObject.Dispatch (this);
		interactCollider.enabled = false;
	}

	public override void Drop ()
	{
		transform.parent = world;
		rigidbody.isKinematic = false;
		interactCollider.enabled = true;
	}

	public override void ThrowWater(bool outside)
	{
		full = false;

		if(outside)
		{
			removeWater.Dispatch(volume);
		}
	}

	public override void ADown ()
	{
		full = true;
	}
	
	public override void AUp ()
	{
		full = false;
	}
	
	public override void BDown ()
	{
	}
	
	public override void BUp ()
	{
	}
}
