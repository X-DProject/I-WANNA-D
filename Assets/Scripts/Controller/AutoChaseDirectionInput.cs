using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ctrl
{
    public sealed class AutoChaseDirectionInput : DirectionInputer
    {
        [Header("chase")]

        [SerializeField]
        private Transform _chaseTarget;

        private void Update()
        {
            this.Direction = (_chaseTarget.position - transform.position).normalized;
        }
    }
}


