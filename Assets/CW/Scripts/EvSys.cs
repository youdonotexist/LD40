using System.Collections.Generic;
using System.Linq;
using CW.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace CW.Scripts
{
    public class EvSys : MonoBehaviour
    {
        //Event types
        // Newspaper delivery (front door)
        // Phone call (family, newspaper)
        // cat box cleaning
        // 
        
        
        [SerializeField] private Text _eventTextbox;
        [SerializeField] private NewsPaperDelivery _newsPaperDeliveryPrefab;
        [SerializeField] private PhoneCallEvent _phoneCallEventPrefab;

        private NewsPaperDelivery _currentDoorEvent = null;
        private PhoneCallEvent _currentPhoneEvent = null;
        

        private readonly List<string> _messageQueue = new List<string>();

        private float _wait = 0.0f;
        
        

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentDoorEvent == null && _wait > 3.0f && Random.value > 0.6f)
            {
                _currentDoorEvent = GameObject.Instantiate(_newsPaperDeliveryPrefab);
                AddMessage(_currentDoorEvent.Message());
                _wait = 0.0f;
            }
            else if (_currentPhoneEvent == null && _wait > 3.0f && Random.value > 0.6f)
            {
                _currentPhoneEvent = GameObject.Instantiate(_phoneCallEventPrefab);
                AddMessage(_currentPhoneEvent.Message());
                _wait = 0.0f;
            }
            else
            {
                _wait += Time.deltaTime;
            }
        }

        private void AddMessage(string message)
        {
            _messageQueue.Add(message);
            _eventTextbox.text = EventsAsString();
        }

        private string EventsAsString()
        {
            return string.Join("\n",  _messageQueue.AsEnumerable().Reverse().ToArray());
        }
    }
}