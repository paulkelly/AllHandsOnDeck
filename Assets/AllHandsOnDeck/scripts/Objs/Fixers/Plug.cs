using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;

public class Plug : IObj
{	
	[Inject]
	public RemoveObject removeObject { get; set; }
	
	[Inject]
	public RespawnPlug respawnPlug { get; set; }

	public Transform world;	
	public Transform obj;
	public Rigidbody theRigidbody;
	
	public override void PickUp(Transform chrItem)
	{
		removeObject.Dispatch (this);
		obj.parent = chrItem;
		obj.localPosition = Vector3.zero;
		obj.localRotation = Quaternion.identity;
		theRigidbody.isKinematic = true;
		gameObject.SetActive (false);
	}
	
	public override void Drop ()
	{
		obj.parent = world;
		theRigidbody.isKinematic = false;
		gameObject.SetActive (true);
	}
	
	public override void Use (Leak leak)
	{
		obj.parent = leak.transform.parent;
		obj.localPosition = Vector3.zero;
		obj.localRotation = Quaternion.identity;
		leak.FixLeak (this);
		respawnPlug.Dispatch();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Pop ();
		}
	}
	public void Pop()
	{
		removeObject.Dispatch (this);
		obj.collider.enabled = false;
		theRigidbody.isKinematic = false;
		theRigidbody.AddForce(Random.Range(0, 40), Random.Range(500, 650), Random.Range(0, 40));
		Destroy (obj.gameObject, 3f);
	}
}