using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CW.Scripts.UI
{
    public class InteractionOptions : MonoBehaviour
    {
        [SerializeField] private Text _choiceBox;

        private Interactable _interactable;
        private Player _interactor;

        private Dictionary<KeyCode, KeyCode> joystickMap = new Dictionary<KeyCode, KeyCode>()
        {
            {KeyCode.Alpha1, KeyCode.JoystickButton0},
            {KeyCode.Alpha2, KeyCode.JoystickButton1},
            {KeyCode.Alpha3, KeyCode.JoystickButton2},
            {KeyCode.Alpha4, KeyCode.JoystickButton3},
        };

        private Dictionary<KeyCode, string> joystickNameMap = new Dictionary<KeyCode, string>()
        {
            {KeyCode.JoystickButton0, "<color=lightblue>X</color>"},
            {KeyCode.JoystickButton1, "<color=yellow>Y</color>"},
            {KeyCode.JoystickButton2, "<color=red>B</color>"},
            {KeyCode.JoystickButton3, "<color=green>A</color>"},
        };

        void Update()
        {
            if (_interactable != null && _interactor != null)
            {
                foreach (KeyCode kc in _interactable.InteractOptions(_interactor).Keys)
                { 
                    if (Input.GetKeyDown(kc) || (joystickMap.ContainsKey(kc) && Input.GetKeyDown(joystickMap[kc])))
                    {
                        Interactable.Interactions i = _interactable.InteractOptions(_interactor)[kc];
                        _interactor.Interact(i, _interactable);
                        _interactable.Interact(_interactor, i);
                        Hide();
                    }
                }

                if (_interactable != null)
                {
                    RectTransform canvasRect = _choiceBox.canvas.GetComponent<RectTransform>();

                    Vector3 viewPos = Camera.main.WorldToViewportPoint(_interactable.transform.position);
                    GetComponent<RectTransform>().anchoredPosition = new Vector2(viewPos.x * canvasRect.sizeDelta.x,
                        viewPos.y * canvasRect.sizeDelta.y);
                }
            }
        }

        public void ShowOptions(Player player, Interactable interactable)
        {
            if (interactable == null)
            {
                Hide();
                return;
            }

            gameObject.SetActive(true);
            _interactable = interactable;
            _interactor = player;

            string choiceList = "";
            Dictionary<KeyCode, Interactable.Interactions> interactions = interactable.InteractOptions(player);
            foreach (KeyCode code in interactions.Keys)
            {
                if (code == KeyCode.None)
                {
                    choiceList += "Press Space to drop what you're carrying";
                    continue;
                }

                string stripped = code.ToString();
                if (stripped.Contains("Alpha"))
                {
                    stripped = stripped.Replace("Alpha", "");
                }

                KeyCode joyMap = KeyCode.None;
                choiceList += stripped;
                if (joystickMap.TryGetValue(code, out joyMap))
                {
                    choiceList += joyMap != KeyCode.None ? " / " + joystickNameMap[joyMap] : "";
                }

                choiceList += ": " + interactions[code] + "\n";
            }

            _choiceBox.text = choiceList;
        }

        private void Hide()
        {
            _interactable = null;
            _interactor = null;
            gameObject.SetActive(false);
        }
    }
}