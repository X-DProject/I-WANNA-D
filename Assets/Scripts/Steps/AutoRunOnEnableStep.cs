using System;
using System.Collections;
using UnityEngine;

namespace Game.Step
{
    public class AutoRunOnEnableStep : LevelStep
    {
        [Header("Behav")]

        [SerializeField]
        private float _delay;

        private void OnEnable()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            yield return new WaitForSecondsRealtime(_delay);
            this.Check();
        }

        protected override bool TryPassStep()
        {
            return true;
        }
    }
}
