using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;

public class Bucket : IObj
{
	[Inject]
	public RemoveWater removeWater { get; set; }

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
	}

	public override void Drop ()
	{
		transform.parent = world;
		rigidbody.isKinematic = false;
	}

	public override void ThrowWater(bool outside)
	{
		full = false;

		if(outside)
		{
			removeWater.Dispatch(volume);
		}

		Debug.Log ("Throw Water: " + outside);
	}

	public override void ADown ()
	{
		full = true;
		Debug.Log ("Bucket Full");
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
