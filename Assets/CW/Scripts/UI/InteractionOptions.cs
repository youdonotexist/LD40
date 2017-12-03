using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CW.Scripts.UI
{
    public class InteractionOptions : MonoBehaviour
    {
        [SerializeField] private Text _choiceBox;

        public void ShowOptions(Dictionary<KeyCode, string> options)
        {
            string choiceList = "";
            foreach (KeyCode code in options.Keys)
            {
                choiceList += code + ": " + options[code] + "\n";
            }

            _choiceBox.text = choiceList;
            _choiceBox.resizeTextForBestFit = true;
        }
    }
}