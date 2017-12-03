using System;
using System.Collections.Generic;
using CW.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace CW.Scripts
{
	public class UIManager : MonoBehaviour
	{

		private static UIManager _uiMan;

		[SerializeField]
		private InteractionOptions _interactionMenu;

		public void ShowOptionsMenu(Player player, Interactable interactable)
		{
			_interactionMenu.ShowOptions(player, interactable);
		}

		public static UIManager Instance()
		{
			if (_uiMan == null)
			{
				_uiMan = GameObject.Find("Canvas").GetComponent<UIManager>();
			}

			return _uiMan;
		}
	}
}
