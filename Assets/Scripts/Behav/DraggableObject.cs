using Game.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Behav
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class DraggableObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UnityEvent _onRelease;

        [SerializeField]
        private UnityEvent _onCancel;

        private bool    _isInDragging = false;
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            // update position
            transform.position = _isInDragging
                ? Mouse.ScreenToWorldPosition
                : _startPosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    _isInDragging = true;
                    _onRelease?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    _isInDragging = false;
                    _onCancel?.Invoke();
                    break;
                default: 
                    break;
            }
        }
    }
}


