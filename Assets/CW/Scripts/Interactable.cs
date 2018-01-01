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
			Answer,
			
			//Toilet
			Flush,
			Flush_Cat,
			
			//Freezer
			Freeze_Cat,
			
			//Trashcan
			Trash_Newspaper,
			
			//Fence
			Free_Cat,
			Scream_At_The_Neighbours,
			
			//TV
			Turn_On,
			Turn_Off
			
		}

		public abstract void Interact(Player player, Interactions interaction);

		public abstract Dictionary<KeyCode, Interactions> InteractOptions(Player player);

		public abstract bool IsAvailable();
		
		public abstract void SetDirection(Vector2 walk);
	}
}
