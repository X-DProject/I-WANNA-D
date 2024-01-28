using System.Collections;
using System.Collections.Generic;
using Game.Behav;
using UnityEngine;

namespace Game.Ctrl
{
    public sealed class KeyboardDirectionInputer : DirectionInputer
    {        
        private void Update()
        {
            this.Direction = new Vector2 (
                    x: Input.GetAxis("Horizontal"),
                    y: Input.GetAxis("Vertical")
                );
        }
    }
}


