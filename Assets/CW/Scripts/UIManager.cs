using CW.Scripts.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CW.Scripts
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _uiMan;

        [SerializeField] private InteractionOptions _interactionMenu;
        [SerializeField] private SpeechBubble _speechBubble;

        [SerializeField] private Text _roundText;
        [SerializeField] private Text _timerText;

        public static UIManager Instance()
        {
            if (_uiMan == null)
            {
                _uiMan = GameObject.Find("Canvas").GetComponent<UIManager>();
            }

            return _uiMan;
        }

        public void ShowOptionsMenu(Player player, Interactable interactable)
        {
            _interactionMenu.ShowOptions(player, interactable);
        }

        public void SetTimer(float timeInSeconds)
        {
            string minutes = Mathf.Floor(timeInSeconds / 60).ToString("00");
            string seconds = Mathf.Floor(timeInSeconds % 60).ToString("00");
            _timerText.text = minutes + ":" + seconds;
        }

        public void SetRound(int metadataRoundCount)
        {
            _roundText.text = "Round " + metadataRoundCount;
        }

        public IObservable<DialogText> ShowText(IObservable<DialogText> stream)
        {
            return _speechBubble.SetText(stream);
        }
    }
}