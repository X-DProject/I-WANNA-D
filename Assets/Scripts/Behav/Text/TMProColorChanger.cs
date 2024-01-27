using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Game.Behav.Text
{
    public sealed class TMProColorChanger : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _text;

        public void ChangeAlphaDirectly(string alphaStr)
        {
            if (string.IsNullOrEmpty(alphaStr))
                throw new ArgumentNullException(nameof(alphaStr));

            _text.alpha = float.Parse(alphaStr);
        }

        public void ChangeAlphaLerply(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentNullException(nameof(arg));

            var alphaTurningInfo = arg.Split(' ');

            if (alphaTurningInfo.Length != 2)
                throw new ArgumentException("[alphaTurningInfo] must have 2 argument for lerp turning.");

            DOTween.To(
                getter: () => _text.alpha,
                setter: a  => _text.alpha = a,
                endValue: float.Parse(alphaTurningInfo[0]),
                duration: float.Parse(alphaTurningInfo[1])
                );
        }
    }
}


