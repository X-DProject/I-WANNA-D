using System.Collections;
using System.Collections.Generic;
using Game.Behav;
using UnityEngine;

namespace Game.Ctrl
{
    public sealed class CameraHelper : MonoBehaviour
    {
        public void PlayAnimationDirectly(string pieceName)
        {
            Camera.main
                .GetComponent<AnimationPlayer>()
                .PlayDirectly(pieceName);
        }
    }
}


