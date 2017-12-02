using UnityEngine;

namespace CW.Scripts.Interactables
{
	public class Door : Interactable
	{

		private bool _isOpen = false;
		private Collider2D _collider2D;
		private Transform _transform;

		void Awake()
		{
			_collider2D = GetComponent<Collider2D>();
			_transform = GetComponent<Transform>();
		}

		public override void Interact(Transform transform)
		{
			Vector3 angles = _transform.localEulerAngles;
			angles.z += _isOpen ? -90.0f : 90.0f;
			_transform.localEulerAngles = angles;

			_isOpen = !_isOpen;
		}
	}
}
