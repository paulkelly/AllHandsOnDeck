using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

namespace AllHandsOnDeck.Character
{
	[System.Serializable]
	public class ThreeDCharacter
	{
		public ThreeDMovement movement;
		public AllHandsOnDeck.InputManager.CharacterController controller;
		public Transform spawnPoint;
		
		public void Spawn(InputDevice device)
		{
			controller.transform.position = spawnPoint.position;	
			controller.character = movement;
			Enable(device);
		}
		
		public void Enable(InputDevice device)
		{
			controller.inputDevice = device;
			controller.gameObject.SetActive(true);
		}
		
		public void Disable()
		{
			controller.gameObject.SetActive(false);
		}
	}
	
	public class ThreeDCharacterSpawner : CharacterSpawner
	{
		public List<ThreeDCharacter> characterPool = new List<ThreeDCharacter>();
		
		void Start()
		{
			foreach(ThreeDCharacter chr in characterPool)
			{
				chr.Disable();
			}
		}
		
		public override float MaxPlayers
		{
			get { return characterPool.Count; }
		}
		
		public override ICharacter GetPlayer(int index)
		{
			return characterPool[index] as ICharacter;
		}
		
		public override void SpawnPlayer(int index, InputDevice device)
		{
			characterPool[index].Spawn(device);
		}
		
		public override void Reset()
		{
			foreach(ThreeDCharacter chr in characterPool)
			{
				chr.Disable();
			}
		}
		
	}
	
}