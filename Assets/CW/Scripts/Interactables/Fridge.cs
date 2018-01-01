using System.Collections.Generic;
using UnityEngine;

namespace CW.Scripts.Interactables
{
    public class Fridge : Interactable
    {
        [SerializeField] private Sprite _closed;

        [SerializeField] private Sprite _open;

        private bool _isOpen = false;
        private SpriteRenderer _spriteRenderer;
        private Interactable _cat;

        private float _killAfter = 10.0f;
        private float _killWait = 0.0f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_cat != null)
            {
                _killWait += Time.deltaTime;
                if (_killWait >= _killAfter)
                {
                    Destroy(_cat.gameObject);
                    EvSys.Instance().AddMessage("Frozen Cat: <color=green>-3 to Cat Attraction</color>");
                    _killWait = 0.0f;
                    _cat = null;
                }
            }
        }

        public override void Interact(Player player, Interactions interaction)
        {
            if (interaction == Interactions.Close || interaction == Interactions.Open)
            {
                _spriteRenderer.sprite = interaction == Interactions.Close ? _closed : _open;

                _isOpen = interaction == Interactions.Open;

                if (_isOpen && _cat != null)
                {       
                    _cat.gameObject.SetActive(true);
                    
                    Vector2 pos = _cat.transform.position;
                    pos.y = _spriteRenderer.bounds.min.y;
                    _cat.transform.position = pos;

                    _cat.transform.parent = null;
                    _cat.Interact(player, Interactions.Drop);
                }
            }
            else if (interaction == Interactions.Drop)
            {
                //do nothing
            }
            else if (interaction == Interactions.Freeze_Cat)
            {
                if (player.IsPickedUpInteractableMurderable())
                {
                    _cat = player.TakePickedupItem() as Cat;
                    if (_cat != null)
                    {
                        _killWait = 0.0f;
                        _cat.gameObject.SetActive(false);
                        _cat.transform.parent = transform;
                        _cat.transform.localPosition = Vector2.zero;
                        Interact(player, Interactions.Close);
                    }
                }
            }
        }

        public override Dictionary<KeyCode, Interactions> InteractOptions(Player player)
        {
            Dictionary<KeyCode, Interactions> interactions = new Dictionary<KeyCode, Interactions>();

            if (player.HasPickedUpInteractable())
            {
                interactions.Add(KeyCode.Alpha1, Interactions.Drop);

                if (player.IsPickedUpInteractableMurderable() && _isOpen && _cat == null)
                {
                    interactions.Add(KeyCode.Alpha2, Interactions.Freeze_Cat);
                }
            }
            else
            {
                interactions.Add(KeyCode.Alpha1, _isOpen ? Interactions.Close : Interactions.Open);
            }

            return interactions;
        }

        public override bool IsAvailable()
        {
            return true;
        }

        public override void SetDirection(Vector2 walk)
        {
        }
    }
}