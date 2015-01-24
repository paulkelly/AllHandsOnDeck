using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace AllHandsOnDeck.Context
{
	public class Bootstrap : ContextView
	{
		void Awake()
		{
			this.context = new AllHandsOnDeckContext (this);
		}
	}
}