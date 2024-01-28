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
        private DirectionInputer _dirCtrller;

        private void FixedUpdate()
        {
            if (_dirCtrller == null)
            {
                Debug.LogWarning("RbMover: missing controller, movement will not work.");
                return;
            }

            var direction = _dirCtrller.Direction;
            
            _rb.AddForce(_speed * direction);
        }
    }
}


