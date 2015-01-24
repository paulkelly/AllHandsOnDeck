using UnityEngine;
using System.Collections;

namespace AllHandsOnDeck.Character
{
	public interface ICharacter
	{
		void Move(Vector2 value);
		
		void Action1Down();
		void Action1Up();
		
		void Action2Down();
		void Action2Up();

		void Action3Down();
		void Action3Up();

		void Action4Down();
		void Action4Up();
	}
}