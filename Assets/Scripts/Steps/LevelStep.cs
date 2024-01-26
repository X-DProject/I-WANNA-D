using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Step
{
    public class LevelStep : MonoBehaviour
    {
        [SerializeField]
        private bool _isOk = false;

        public bool IsOk
        {
            get => _isOk; 
            protected set
            {
                if (value)
                    Debug.Log($"[Step] step {name} is finished.");
                _isOk = value;
            }
        }
    }
}
