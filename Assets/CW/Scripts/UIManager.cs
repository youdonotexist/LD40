using System.Collections.Generic;
using CW.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CW.Scripts
{
	public class UiManager : MonoBehaviour
	{

		private static UiManager _uiMan;

		[SerializeField]
		private InteractionOptions _interactionMenu;

		public void ShowOptionsMenu(Dictionary<KeyCode, string> options)
		{
			_interactionMenu.ShowOptions(options);
		}

		public static UiManager Instance()
		{
			if (_uiMan == null)
			{
				_uiMan = GameObject.Find("Canvas").GetComponent<UiManager>();
			}

			return _uiMan;
		}
	}
}
