using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Behav
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class DraggableObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UnityEvent _onRelease;

        private bool    _isInDragging = false;
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            // update position
            if (_isInDragging)
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));

                transform.position = screenToWorld;
            }
            else
            {
                transform.position = _startPosition;
            }
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
                    break;
                default: 
                    break;
            }
        }
    }
}


