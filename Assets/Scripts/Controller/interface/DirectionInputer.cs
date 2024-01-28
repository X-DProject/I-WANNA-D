using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Ctrl
{
    public abstract class DirectionInputer : MonoBehaviour
    {
        [field: Header("debug")]

        [field: SerializeField]
        public Vector2 Direction { get; protected set; }
    }   
}


