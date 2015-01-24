using UnityEngine;
using System.Collections;

namespace AllHandsOnDeck.Context
{
	public class AllHandsOnDeckContext : SignalContext 
	{
		public AllHandsOnDeckContext(MonoBehaviour contextView) : base(contextView)
		{
		}
		
		protected override void mapBindings()
		{
		}
	}
}