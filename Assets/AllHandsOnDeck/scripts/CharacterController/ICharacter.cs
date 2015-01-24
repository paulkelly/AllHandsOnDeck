using UnityEngine;
using System.Collections;

namespace AllHandsOnDeck.Character
{
	public interface ICharacter
	{
		void Move(Vector2 value);
		
		void Action1();
		
		void Action2();
		
		void Action3();
		
		void Action4();
	}
}