using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjCollider : IObj
{
	private IObj nearestObj;
	private List<IObj> objectsInRange = new List<IObj>();

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
		float minDistance = 0;
		foreach(IObj obj in objectsInRange)
		{
			float dist = Vector3.Distance(obj.transform.position, transform.position);
			if(dist > minDistance)
			{
				minDistance = dist;
				nearestObj = obj;
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
