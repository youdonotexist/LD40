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
            if (_interactable != null)
            {
                foreach (KeyCode kc in _interactable.InteractOptions().Keys)
                {
                    if (Input.GetKeyDown(kc))
                    {
                       // _interactor.OnInteraction(kc);
                       // _interactable.OnInteraction()
                    }
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
            else
            {
                gameObject.SetActive(true);
            }

            string choiceList = "";
            foreach (KeyCode code in interactable.InteractOptions().Keys)
            {
                choiceList += code + ": " + interactable.InteractOptions()[code] + "\n";
            }

            _choiceBox.text = choiceList;
            _choiceBox.resizeTextForBestFit = true;

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