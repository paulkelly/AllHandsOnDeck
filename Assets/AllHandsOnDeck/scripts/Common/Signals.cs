using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace AllHandsOnDeck.Common
{
	public class AddWater : Signal<float>
	{
	}

	public class RemoveWater : Signal<float>
	{
	}

	public class SpringLeak : Signal<Leak>
	{
	}

	public class FixLeak : Signal<Leak>
	{
	}

	public class RemoveObject : Signal<IObj>
	{
	}
	
	public class StartGame : Signal
	{
	}
	
	public class EndGame : Signal
	{
	}
	
	public class RollCredits : Signal<float>
	{
	}
	
	public class RespawnPlug : Signal
	{
	}
}