using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System;

namespace AllHandsOnDeck.Character
{
	[RequireComponent (typeof(Rigidbody))]
	public class ThreeDMovement : View, ICharacter
	{
		[Range (0,20)]
		public float speed;
		
		[Range (0,400)]
		public float turnSpeed;

		public Animator animator;
		public ObjCollider objCollider;

		public Transform item;

		private bool hasItem;
		private IObj holding;

		public PartEffectEnabler throwWaterEffect;

		public float animatorSpeed
		{
			set
			{
				animator.SetFloat("speed", value);
			}
		}

		public bool stopLeak
		{
			get
			{
				return animator.GetBool("stop");
			}

			set
			{
				animator.SetBool("stop", value);
			}
		}

		public bool Bucket
		{
			get
			{
				return animator.GetBool("bucket");
			}

			set
			{
				animator.SetBool("bucket", value);
			}
		}

		public void Grab()
		{
			animator.SetTrigger ("grab");
		}

		public void CollectWater()
		{
			bucketFull = true;
			CollectingWater = true;
			holding.ADown();
		}

		public bool CollectingWater
		{
			get
			{
				return false;
			}

			set
			{

			}
		}

		public void ThrowWater()
		{
			ReleaseWater ();
		}
		
		void Start ()
		{
			
		}
		
		void Update ()
		{

		}
		
		#region ICharacter implementation
			
		public void Move (Vector2 value)
		{
			Vector3 translation = new Vector3 (value.x, 0, -value.y);
			
			translation = Camera.main.cameraToWorldMatrix.MultiplyVector (translation);
			
			translation.Set (translation.x, 0, translation.z);
			
			if(translation.magnitude > Mathf.Epsilon && !stopLeak)
			{
				Rotate (translation);			
			}
			//transform.Translate (translation * speed * Time.deltaTime, Space.World);
			//rigidbody.MovePosition (rigidbody.position + translation * speed * Time.deltaTime);
			animatorSpeed = translation.magnitude;

			//Debug.DrawRay (head.position, aim.position - head.position, Color.red);
		}
		
		private void Rotate(Vector3 dir)
		{
			//transform.LookAt(dir);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
		}
	
		public void Action1Down ()
		{
			if(hasItem)
			{
				if(isHoldingBucket)
				{
					CollectWater();
				}
			}
			else
			{
				if(objCollider.isNearestColliderLeak)
				{
					holding = objCollider.nearestObj;
					hasItem = true;
					stopLeak = true;
					objCollider.ADown ();
				}
				if(objCollider.isNearestColliderBucket)
				{
					Grab();
				}
			}
		}

		public bool isHoldingBucket
		{
			get
			{
				if(holding != null)
				{
					Bucket bucket = null;
					try
					{
						bucket = (Bucket) holding;
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

		public bool isFixingLeak
		{
			get
			{
				if(holding != null)
				{
					Leak leak = null;
					try
					{
						leak = (Leak) holding;
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

		public void PickUp()
		{
			if(objCollider.isNearestColliderBucket)
			{
				holding = objCollider.nearestObj;
				holding.PickUp(item);
				hasItem = true;

				if(isHoldingBucket)
				{
					Bucket = true;
				}
			}
		}

		public void Action1Up ()
		{
			if(hasItem)
			{
				if(isHoldingBucket)
				{
					if(bucketFull)
					{
						ThrowWater();
						bucketFull = false;
					}
					else
					{
						CollectingWater = false;
					}
				}
				else if(isFixingLeak)
				{
					stopLeak = false;
					hasItem = false;
					holding.AUp();
					holding = null;
				}
			}
			else
			{
				stopLeak = false;
				objCollider.AUp ();
			}
		}

		private bool bucketFull = false;
		public void FillBucket()
		{
			if(hasItem)
			{
				if(isHoldingBucket)
				{
					holding.ADown();
					bucketFull = true;
				}
			}
		}
	
		public Transform ship;
		public Transform head;
		public Transform aim;
		public void ReleaseWater()
		{
			throwWaterEffect.Enabled = true;

			Ray ray = new Ray(head.position, aim.position - head.position);

			RaycastHit[] hits = Physics.RaycastAll (ray);

			bool onShip = false;
			foreach(RaycastHit hit in hits)
			{
				if(hit.collider.transform.GetHashCode() == ship.GetHashCode())
				{
					onShip = true;
				}
			}

			if(hasItem)
			{
				if(isHoldingBucket)
				{
					holding.ThrowWater(!onShip);
				}
			}
		}

		public void Action2Down ()
		{
			if(hasItem)
			{
				holding.Drop();
				hasItem = false;
				Bucket = false;
				holding = null;
			}
		}

		public void Action2Up ()
		{
		}
	
		public void Action3Down ()
		{
		}

		public void Action3Up ()
		{
		}
	
		public void Action4Down ()
		{
		}

		public void Action4Up ()
		{
		}
	
		#endregion
	}
}