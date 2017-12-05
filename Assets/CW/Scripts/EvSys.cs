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

        private readonly List<string> _messageQueue = new List<string>();
        private readonly RoundMetadata[] _roundMetadataList = new RoundMetadata[3];
        private int _currentRound = 0;
        private float _roundCounter;
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
            _roundMetadataList[0] = new RoundMetadata
            {
                NewspaperFrequency = 10.0f,
                PhoneCallFrequency = 10.0f,
                RoundDuration = 60 * 2,
                RoundCount = 0
            };

            _roundMetadataList[1] = new RoundMetadata
            {
                NewspaperFrequency = 8.0f,
                PhoneCallFrequency = 8.0f,
                RoundDuration = 60 * 3,
                RoundCount = 1
            };

            _roundMetadataList[2] = new RoundMetadata
            {
                NewspaperFrequency = 5.0f,
                PhoneCallFrequency = 5.0f,
                RoundDuration = 60 * 3,
                RoundCount = 2
            };
        } 

        // Update is called once per frame
        void Update()
        {   
            if (_currentRound > -1)
            {
                if (_currentDoorEvent == null && _wait > 3.0f && Random.value > 0.6f &&
                    CatMeter.TotalNewspapers < CatMeter.MaxNewspapers)
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
                
                _roundCounter = Mathf.Max(_roundCounter - Time.deltaTime, 0.0f);
                UIManager.Instance().SetTimer(_roundCounter);

                if (_roundCounter <= 0.0f)
                {
                    StartRound();
                }
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

        private void StartRound()
        {
            _currentRound++;
            RoundMetadata metadata = _roundMetadataList[_currentRound];
            
            _roundCounter = metadata.RoundDuration;
            UIManager.Instance().SetTimer(metadata.RoundDuration);
            UIManager.Instance().SetRound(metadata.RoundCount);
        }
    }
}