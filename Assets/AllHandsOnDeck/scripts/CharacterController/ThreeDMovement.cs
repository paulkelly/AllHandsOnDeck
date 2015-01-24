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
			
			if(translation.magnitude > Mathf.Epsilon)
			{
				Rotate (translation);			
			}
			transform.Translate (translation * speed * Time.deltaTime, Space.World);
		}
		
		private void Rotate(Vector3 dir)
		{
			//transform.LookAt(dir);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
			
		}
	
		public void Action1 ()
		{
		}
	
		public void Action2 ()
		{
		}
	
		public void Action3 ()
		{
		}
	
		public void Action4 ()
		{
		}
	
		#endregion
	}
}