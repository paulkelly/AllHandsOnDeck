using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System;
using AllHandsOnDeck.Common;

namespace AllHandsOnDeck.Character
{
	[RequireComponent (typeof(Rigidbody))]
	public class ThreeDMovement : View, ICharacter
	{
		[Inject]
		public EndGame endgame { get; set; }
		
		private bool gameOver = false;
		private void GameOver()
		{
			gameOver = true;
		}
		
		
	
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
			CollectingWater = true;
			holding.ADown();
		}

		public bool CollectingWater
		{
			get
			{
				return animator.GetBool("skoop");
			}

			set
			{
				animator.SetBool("skoop", value);
			}
		}

		public void ThrowWater()
		{
			animator.SetTrigger ("throw");
		}

		public bool Plug
		{
			get
			{
				return animator.GetBool("plug");
			}
			
			set
			{
				animator.SetBool("plug", value);
			}
		}

		public void Fix()
		{
			animator.SetTrigger ("fix");
		}
		
		protected override void OnStart ()
		{
			endgame.AddListener(GameOver);
		}
		
		void Update ()
		{

		}
		
		#region ICharacter implementation
			
		public void Move (Vector2 value)
		{
			if(gameOver)
			{
				animatorSpeed = 0;
				return;
			}
		
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
			if(gameOver)
			{
				return;
			}
		
			if(hasItem)
			{
				if(isHoldingBucket)
				{
					CollectWater();
				}
				if(isHoldingPlug)
				{
					Fix();
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
				if(objCollider.isNearestColliderBucket || objCollider.isNearestColliderPlug)
				{
					if(objCollider.isNearestColliderPlug)
					{
						Plug = true;
					}

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

		public bool isHoldingPlug
		{
			get
			{
				if(holding != null)
				{
					Plug plug = null;
					try
					{
						plug = (Plug) holding;
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
			if(objCollider.isNearestColliderBucket || objCollider.isNearestColliderPlug)
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

		public void PlugLeak()
		{
			if(objCollider.isNearestColliderLeak)
			{
				holding.ADown();
				//objCollider.FixLeak();
				hasItem = false;
				holding.Use((Leak) objCollider.nearestObj);
				holding = null;
			}
		}

		public void Action1Up ()
		{
			if(gameOver)
			{
				return;
			}
		
			if(hasItem)
			{
				if(isHoldingBucket)
				{
					if(bucketFull)
					{
						ThrowWater();
						bucketFull = false;
						CollectingWater = false;
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
			if(gameOver)
			{
				return;
			}
		
			if(hasItem)
			{
				holding.Drop();
				hasItem = false;
				Bucket = false;
				holding = null;
				Plug = false;
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