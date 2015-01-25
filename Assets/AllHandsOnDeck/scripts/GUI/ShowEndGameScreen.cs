using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using AllHandsOnDeck.Common;
using UnityEngine.UI;

public class ShowEndGameScreen : View
{
	[Inject]
	public RollCredits endGame { get; set; }
	
	public CanvasGroupFader canvasGroup;
	public CanvasGroupFader black;
	
	public Text surviveText;
	public string surviveStart = "You survived for ";
	public string surviveEnd = " seconds";
	
	public Text topScore;
	public string topScoreStart = "Top score is ";
	public string topScoreEnd = " seconds";
	
	protected override void OnStart()
	{
		endGame.AddListener(ShowLeaderboard);
	}
	
	void Update()
	{
		if(canvasGroup.Visible)
		{
			if(InControl.InputManager.ActiveDevice.MenuWasPressed)
			{
				black.Visible = true;
			}
			
			if(black.Visible)
			{
				Application.LoadLevel(0);
			}
		}
	}
	
	private void ShowLeaderboard(float surviveTime)
	{
		surviveText.text = surviveStart + Mathf.FloorToInt(surviveTime) + surviveEnd;
		topScore.text = topScoreStart + Mathf.FloorToInt(GetTopScore(surviveTime)) + topScoreEnd;
		canvasGroup.Visible = true;	
	}
	
	private string key = "AllHandsLeader";
	
	private float GetTopScore(float surviveTime)
	{
		//PlayerPrefs.DeleteKey(key);
		float topscore = 0;
		if(PlayerPrefs.HasKey(key))
		{
			topscore = PlayerPrefs.GetFloat(key);
			
			if(topscore < surviveTime)
			{
				PlayerPrefs.SetFloat(key, surviveTime);
				topscore = surviveTime;
			}
		}
		else
		{
			PlayerPrefs.SetFloat(key, surviveTime);
			topscore = surviveTime;
		}
		return topscore;
	}
}
