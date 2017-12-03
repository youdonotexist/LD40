using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts
{
	public abstract class Interactable : MonoBehaviour
	{
		public enum Interactions
		{
			//None
			None,
			
			//Player Only
			Pickup,
			Drop,
			Kill,
			
			//Cat
			Pet,
			
			//Door
			Open,
			Close,
			
			//Phone
			Answer
		}

		public abstract void Interact(Player player, Interactions interaction);

		public abstract Dictionary<KeyCode, Interactions> InteractOptions(Player player);

		public abstract bool IsAvailable();
	}
}
