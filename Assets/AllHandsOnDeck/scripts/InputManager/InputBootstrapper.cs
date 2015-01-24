using UnityEngine;
using System.Collections;

namespace goshdarngames.InputManager
{
	public class InputBootstrapper : MonoBehaviour
	{	
		void Start ()
		{
			InControl.InputManager.Setup();
		}
		
		void Update()
		{
			InControl.InputManager.Update();
		}
	}
}