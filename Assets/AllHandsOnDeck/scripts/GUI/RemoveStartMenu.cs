using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using AllHandsOnDeck.Common;

public class RemoveStartMenu : View
{
	[Inject]
	public StartGame startGame { get; set; }
	
	public CanvasGroupFader canvasGroup;
	
	public CanvasGroupFader black;
		
	protected override void OnStart()
	{
		startGame.AddListener(StartTheGame);
		
		black.Visible = false;
	}
	
	private void StartTheGame()
	{
		canvasGroup.Visible = false;	
	}
}
