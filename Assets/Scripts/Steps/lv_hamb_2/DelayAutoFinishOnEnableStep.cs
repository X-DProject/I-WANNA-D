using System;
using System.Collections;
using UnityEngine;

namespace Game.Step
{
    public class DelayAutoFinishOnEnableStep : LevelStep
    {
        [Header("Behav")]

        [SerializeField]
        private float _finishDelayTime;

        private void OnEnable()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            yield return new WaitForSecondsRealtime(_finishDelayTime);
            this.Check();
        }

        protected override bool TryPassStep()
        {
            return true;
        }
    }
}
