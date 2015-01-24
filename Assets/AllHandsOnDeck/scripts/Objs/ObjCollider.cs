using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AllHandsOnDeck.Common;
using System;

public class ObjCollider : IObj
{
	[Inject]
	public FixLeak fixLeak { get; set; }

	[Inject]
	public RemoveObject removeObject { get; set; }

	public IObj nearestObj;
	private List<IObj> objectsInRange = new List<IObj>();

	protected override void OnStart()
	{
		fixLeak.AddListener (RemoveLeak);

		removeObject.AddListener (Remove);
	}

	private void Remove(IObj obj)
	{
		if(objectsInRange.Contains(obj))
		{
			objectsInRange.Remove(obj);
		}
	}

	private void RemoveLeak(Leak leak)
	{
		if(objectsInRange.Contains(leak))
		{
			objectsInRange.Remove (leak);
		}
	}

	public bool isNearestColliderLeak
	{
		get
		{
			if(nearestObj != null)
			{
				Leak leak = null;
				try
				{
					leak = (Leak) nearestObj;
				}
				catch(InvalidCastException e)
				{
				}

				if(leak != null)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool isNearestColliderBucket
	{
		get
		{
			if(nearestObj != null)
			{
				Bucket bucket = null;
				try
				{
					bucket = (Bucket) nearestObj;
				}
				catch(InvalidCastException e)
				{
				}
				if(bucket != null)
				{
					return true;
				}
			}
			return false;
		}
	}

	public bool isNearestColliderPlug
	{
		get
		{
			if(nearestObj != null)
			{
				Plug plug = null;
				try
				{
					plug = (Plug) nearestObj;
				}
				catch(InvalidCastException e)
				{
				}
				if(plug != null)
				{
					return true;
				}
			}
			return false;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		IObj obj = collider.transform.GetComponent<IObj> ();
		if(obj != null)
		{
			objectsInRange.Add(obj);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		IObj obj = collider.GetComponent<IObj> ();
		if(obj != null)
		{
			objectsInRange.Remove(obj);
		}
	}

	void Update()
	{
		float minDistance = float.MaxValue;
		nearestObj = null;
		foreach(IObj obj in objectsInRange)
		{
			if(obj.gameObject.activeSelf)
			{
				float dist = Vector3.Distance(obj.transform.position, transform.position);
				if(dist < minDistance)
				{
					minDistance = dist;
					nearestObj = obj;
				}
			}
		}
	}

	public override void FixLeak(Plug plug)
	{
		if(nearestObj != null)
		{
			nearestObj.FixLeak(plug);
		}
	}

	public override void PickUp (Transform chrItem)
	{
		if(nearestObj != null)
		{
			nearestObj.PickUp(chrItem);
		}
	}


	public override void ADown ()
	{
		if(nearestObj != null)
		{
			nearestObj.ADown();
		}
	}
	
	public override void AUp ()
	{
		if(nearestObj != null)
		{
			nearestObj.AUp();
		}
	}
	
	public override void BDown ()
	{
		if(nearestObj != null)
		{
			nearestObj.BDown();
		}
	}
	
	public override void BUp ()
	{
		if(nearestObj != null)
		{
			nearestObj.BUp();
		}
	}
}
