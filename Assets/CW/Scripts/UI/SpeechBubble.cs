using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace CW.Scripts.UI
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private SpriteRenderer _player;

        public void SetText2(string text)
        {
            Debug.Log(text);
            gameObject.SetActive(!string.IsNullOrEmpty(text));
            _text.text = text;
        }

        public void SetText(UniRx.IObservable<DialogText> events)
        {
            /*events
                .Select(e => Observable.Return(e.Text).Delay(TimeSpan.FromSeconds(e.Duration)))
                .Do(e => SetText2(e.Text))
                .Concat()
                .DoOnCompleted(() => gameObject.SetActive(false))
                .Subscribe(SetText2);*/
            
            Observable.Interval(TimeSpan.FromSeconds(2.0f))
                .Zip(events, (n, p) => p)     
                .Subscribe(x => SetText2(x.Text));
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