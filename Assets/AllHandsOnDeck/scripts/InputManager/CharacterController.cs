using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Character;
using InControl;

namespace AllHandsOnDeck.InputManager
{
	public class CharacterController : MonoBehaviour
	{
		public InputDevice inputDevice;
		public ICharacter character;
	
		void Update ()
		{				
			if(inputDevice == null)
			{
				return;
			}
			
			if(character == null)
			{
				return;
			}
			
			character.Move(inputDevice.Direction);
		
			if(inputDevice.Action1.WasPressed)
			{
				character.Action1Down();
			}
			if(inputDevice.Action1.WasReleased)
			{
				character.Action1Up();
			}


			if(inputDevice.Action2.WasPressed)
			{
				character.Action2Down();
			}
			if(inputDevice.Action2.WasReleased)
			{
				character.Action2Up();
			}

			if(inputDevice.Action3.WasPressed)
			{
				character.Action3Down();
			}
			if(inputDevice.Action3.WasReleased)
			{
				character.Action3Up();
			}


			if(inputDevice.Action4.WasPressed)
			{
				character.Action4Down();
			}
			if(inputDevice.Action4.WasReleased)
			{
				character.Action4Up();
			}
		}
	}
}