using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AllHandsOnDeck.Common;

public class ObjCollider : IObj
{
	[Inject]
	public FixLeak fixLeak { get; set; }

	private IObj nearestObj;
	private List<IObj> objectsInRange = new List<IObj>();

	protected override void OnStart()
	{
		fixLeak.AddListener (RemoveLeak);
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
				Leak leak = (Leak) nearestObj;
				if(leak != null)
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
