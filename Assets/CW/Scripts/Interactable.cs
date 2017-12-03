using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts
{
	public abstract class Interactable : MonoBehaviour
	{
		public enum Interactions
		{
			//Player Only
			Pickup,
			Drop,
			Kill,
			
			//Cat
			Pet,
			
			//Door
			Open,
			Close
		}

		public abstract void Interact(Player player, Interactions interaction);

		public abstract Dictionary<KeyCode, Interactions> InteractOptions(Player player);
	}
}
