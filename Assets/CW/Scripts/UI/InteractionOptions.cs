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

        void Update()
        {
            if (_interactable != null && _interactor != null)
            {
                foreach (KeyCode kc in _interactable.InteractOptions(_interactor).Keys)
                {
                    if (!Input.GetKeyDown(kc)) continue;
                    Interactable.Interactions i = _interactable.InteractOptions(_interactor)[kc];
                    _interactor.Interact(i, _interactable);
                    _interactable.Interact(_interactor, i);
                    Hide();
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
                string stripped = code.ToString();
                if (stripped.Contains("Alpha"))
                {
                    stripped = stripped.Replace("Alpha", "");
                }
                choiceList += stripped + ": " + interactions[code] + "\n";
            }

            _choiceBox.text = choiceList;

            RectTransform canvasRect = _choiceBox.canvas.GetComponent<RectTransform>();

            Vector3 viewPos = Camera.main.WorldToViewportPoint(interactable.transform.position);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(viewPos.x * canvasRect.sizeDelta.x,
                viewPos.y * canvasRect.sizeDelta.y);
        }

        private void Hide()
        {
            _interactable = null;
            _interactor = null;
            gameObject.SetActive(false);
        }
    }
}