using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System.Collections.Generic;
using InControl;
using AllHandsOnDeck.Common;

public class PlayerSetup : View
{
	[Inject]
	public StartGame startGame { get; set; }

	public CharacterSpawner spawner;
		
	Dictionary<InputDevice, int> players = new Dictionary<InputDevice, int>();
	private int next = 0;
	
	void Update ()
	{
		if(InControl.InputManager.ActiveDevice.MenuWasPressed)
		{
			if(players.Count == 0)
			{
				startGame.Dispatch();
			}
		
			InputDevice activeDevice = InControl.InputManager.ActiveDevice;
			if(!players.ContainsKey(activeDevice))
			{
				AddPlayer(activeDevice);
			}
		}
	}
	
	private void AddPlayer(InputDevice activeDevice)
	{
		players.Add(activeDevice, next);
		spawner.SpawnPlayer(next, InControl.InputManager.ActiveDevice);
		next++;
	}
	
	private void Reset()
	{
		next = 0;
		players.Clear();
	}
}
