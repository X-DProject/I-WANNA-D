using System.Collections;
using System.Collections.Generic;
using Game.Ctrl;
using UnityEngine;

namespace Game.Behav
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class RbMover : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private float _speed;

        [Header("Ctrl")]

        [SerializeField]
        private KeyboardInputer _controller;

        private void Update()
        {
            if (_controller == null)
            {
                Debug.LogWarning("RbMover: missing controller, movement will not work.");
                return;
            }

            var direction = _controller.InputDirection;

            _rb.AddForce(_speed * direction);
        }
    }
}


