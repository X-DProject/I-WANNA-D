using System;
using System.Collections;
using UnityEngine;

namespace Game.Step.LvHamb2
{
    public sealed class CompleteLevelOnEnableStep : LevelStep
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

        protected override sealed bool TryPassStep()
        {
            try
            {
                GameInstance.Signal("next_level");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }       
            return true;
        }
    }
}
