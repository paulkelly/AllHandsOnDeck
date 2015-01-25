using UnityEngine;
using strange.extensions.mediation.impl;

public class CanvasGroupFader : View
{
	public float fadeTime = 0.5f;
	
	public CanvasGroup _canvasGroup;
	
	public bool initialyVisible;
	
	protected override void OnAwake()
	{
		if(!initialyVisible)
		{
			_canvasGroup.alpha = 0;
			_canvasGroup.gameObject.SetActive(_visible);
			_canvasGroup.blocksRaycasts = _visible;
		}
		else
		{
			_visible = true;
			_canvasGroup.alpha = 1;
			_canvasGroup.gameObject.SetActive(_visible);
		}
	}
	
	private bool _visible = false;
	
	public bool Visible
	{
		get
		{
			return _visible;
		}
		
		set
		{
			if(value)
			{
				Enable();
			}
			else
			{
				Disable();
			}
		}
	}
	
	private float targetAlpha = 0;
	private float initalAlpha = 0;
	
	private bool _fadeing = false;
	private float time = 0;
	
	private void Enable()
	{
		if(!_visible)
		{
			_visible = true;
			targetAlpha = 1;
			time = 0;
			_fadeing = true;
			if(gameObject.activeSelf)
			{
				initalAlpha = _canvasGroup.alpha;
			}
			else
			{
				initalAlpha = 0;
				gameObject.SetActive(true);
				_canvasGroup.alpha = initalAlpha;
			}
		}
	}
	
	private void Disable()
	{
		if(_visible)
		{
			_visible = false;
			targetAlpha = 0;
			initalAlpha = _canvasGroup.alpha;
			time = 0;
			_fadeing = true;
		}
	}
	
	void Update()
	{
		if(_fadeing)
		{
			time += Time.deltaTime;
			float percComplete = time / fadeTime;
			if(percComplete < 1)
			{
				_canvasGroup.alpha = initalAlpha + ((targetAlpha - initalAlpha) * percComplete);
			}
			else
			{
				_fadeing = false;
				_canvasGroup.alpha = targetAlpha;
				gameObject.SetActive(_visible);
				_canvasGroup.blocksRaycasts = _visible;
			}
		}
	}
}
