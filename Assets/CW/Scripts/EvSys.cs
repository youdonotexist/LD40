using System.Collections.Generic;
using System.Linq;
using CW.Scripts.Events;
using CW.Scripts.UI;
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

        [SerializeField] private CircleCollider2D _newsPaperDeliverySpawnPoint;

        private NewsPaperDelivery _currentDoorEvent;
        private PhoneCallEvent _currentPhoneEvent;
        
        private RoundMetadata[] _roundMetadataList = new RoundMetadata[3];
        private int _currentRound = 0;
        
        private readonly List<string> _messageQueue = new List<string>();

        private float _wait = 0.0f;

        private static EvSys _this;

        public static EvSys Instance()
        {
            if (_this == null)
            {
                GameObject go = GameObject.Find("EvSys");
                if (go != null)
                {
                    _this = go.GetComponent<EvSys>();  
                }
            }

            return _this;
        }

        void Start()
        {
            _roundMetadataList[0] = new RoundMetadata();
            _roundMetadataList[0].NewspaperFrequency = 10.0f;
            _roundMetadataList[0].PhoneCallFrequency = 10.0f;
            
            _roundMetadataList[1].NewspaperFrequency = 8.0f;
            _roundMetadataList[1].PhoneCallFrequency = 8.0f;
            
            _roundMetadataList[2].NewspaperFrequency = 5.0f;
            _roundMetadataList[2].PhoneCallFrequency = 5.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentDoorEvent == null && _wait > 3.0f && Random.value > 0.6f && CatMeter.TotalNewspapers < CatMeter.MaxNewspapers)
            {
                _currentDoorEvent = Instantiate(_newsPaperDeliveryPrefab);
                _currentDoorEvent.SetSpawnPoint(_newsPaperDeliverySpawnPoint);
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

        public void AddMessage(string message)
        {
            _messageQueue.Add(message);
            _eventTextbox.text = EventsAsString();
        }

        private string EventsAsString()
        {
            return string.Join("\n", _messageQueue.AsEnumerable().Reverse().ToArray());
        }
    }
}