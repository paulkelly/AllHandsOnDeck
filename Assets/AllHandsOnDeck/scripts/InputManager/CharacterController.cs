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
				character.Action1();
			}
			if(inputDevice.Action2)
			{
				character.Action2();
			}
			if(inputDevice.Action3)
			{
				character.Action3();
			}
			if(inputDevice.Action4)
			{
				character.Action4();
			}
		}
	}
}