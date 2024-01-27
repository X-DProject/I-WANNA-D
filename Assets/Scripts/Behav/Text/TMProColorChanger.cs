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
                throw new ArgumentException("[alphaTurningInfo] must have 2 argument for alpha lerp turning: alpha duration");

            DOTween.To(
                getter: () => _text.alpha,
                setter: a  => _text.alpha = a,
                endValue: float.Parse(alphaTurningInfo[0]),
                duration: float.Parse(alphaTurningInfo[1])
                );
        }

        public void ChangeColorLerply(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                throw new ArgumentNullException(nameof(arg));

            var colorTurningInfo = arg.Split(' ');

            if (colorTurningInfo.Length != 4)
                throw new ArgumentException("[alphaTurningInfo] must have 4 argument for color lerp turning: r g b duration");

            DOTween.To(
                getter: () => _text.color,
                setter: c => _text.color = c,
                endValue: new Color(
                    r: float.Parse(colorTurningInfo[0]) / 255,
                    g: float.Parse(colorTurningInfo[1]) / 255,
                    b: float.Parse(colorTurningInfo[2]) / 255
                    ),
                duration: float.Parse(colorTurningInfo[3])
                );
        }
    }
}


