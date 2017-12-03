using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts
{
	public abstract class Interactable : MonoBehaviour
	{

		public abstract void Interact(Transform transform, KeyCode keycode);

		public abstract Dictionary<KeyCode, string> InteractOptions();


	}
}
