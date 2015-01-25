using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using AllHandsOnDeck.Common;

public class PlugRespawner : View
{
	[Inject]
	public RespawnPlug respawnPlug { get; set; }
	
	public GameObject plug;
	
	protected override void OnStart()
	{
		respawnPlug.AddListener(Respawn);
	}
	
	private void Respawn()
	{
		GameObject obj = GameObject.Instantiate(plug, transform.position, Quaternion.identity) as GameObject;
		
		obj.transform.parent = transform;
	}
}
