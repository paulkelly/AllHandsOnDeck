using UnityEngine;
using System.Collections;
using AllHandsOnDeck.Common;

namespace AllHandsOnDeck.Context
{
	public class AllHandsOnDeckContext : SignalContext 
	{
		public AllHandsOnDeckContext(MonoBehaviour contextView) : base(contextView)
		{
		}
		
		protected override void mapBindings()
		{
			base.mapBindings ();

			injectionBinder.Bind<AddWater> ().ToSingleton ();
			injectionBinder.Bind<RemoveWater> ().ToSingleton ();

			injectionBinder.Bind<SpringLeak> ().ToSingleton ();
			injectionBinder.Bind<FixLeak> ().ToSingleton ();

			injectionBinder.Bind<RemoveObject> ().ToSingleton ();
			
			injectionBinder.Bind<RespawnPlug>().ToSingleton();
			
			injectionBinder.Bind<StartGame>().ToSingleton();
			injectionBinder.Bind<EndGame>().ToSingleton();
			injectionBinder.Bind<RollCredits>().ToSingleton();
		}
	}
}