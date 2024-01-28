using System.Collections;
using System.Collections.Generic;
using Game.Behav;
using UnityEngine;

namespace Game.Ctrl
{
    public sealed class KeyboardInputer : MonoBehaviour
    {
        [field: Header("debug")]

        [field: SerializeField]
        public Vector2 InputDirection { get; private set; }

        private void Update()
        {
            InputDirection = new Vector2 (
                    x: Input.GetAxis("Horizontal"),
                    y: Input.GetAxis("Vertical")
                );
        }
    }
}


