using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace CW.Scripts.UI
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private SpriteRenderer _player;

        public void SetText2(DialogText dialogText)
        {
            bool emptyDialog = dialogText == null || string.IsNullOrEmpty(dialogText.Text);

            gameObject.SetActive(!emptyDialog);
            _text.text = !emptyDialog ? dialogText.Text : null;
        }

        public UniRx.IObservable<DialogText> SetText(UniRx.IObservable<DialogText> events)
        {
            var stream =
                Observable.Zip
                    (
                        Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(2.0f)).Select(l => (DialogText) null),
                        events
                    )
                    .Concat()
                    .DoOnCompleted(() => gameObject.SetActive(false))
                    .SelectMany((empty, good) => empty);


            stream.Subscribe(SetText2);

            return stream;
        }

        private void LateUpdate()
        {
            if (_player == null) return;

            RectTransform canvasRect = _text.canvas.GetComponent<RectTransform>();

            Vector3 viewPos = Camera.main.WorldToViewportPoint(_player.bounds.max);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(viewPos.x * canvasRect.sizeDelta.x,
                viewPos.y * canvasRect.sizeDelta.y);
        }
    }
}