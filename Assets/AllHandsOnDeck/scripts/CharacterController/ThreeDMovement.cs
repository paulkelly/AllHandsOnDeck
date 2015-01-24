using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

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
		}
		
		private void Rotate(Vector3 dir)
		{
			//transform.LookAt(dir);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
		}
	
		public void Action1Down ()
		{
			if(objCollider.isNearestColliderLeak)
			{
				stopLeak = true;
			}
			objCollider.ADown ();
		}

		public void Action1Up ()
		{
			stopLeak = false;
			objCollider.AUp ();
		}
	
		public void Action2Down ()
		{
			objCollider.BDown ();
		}

		public void Action2Up ()
		{
			objCollider.BUp ();
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