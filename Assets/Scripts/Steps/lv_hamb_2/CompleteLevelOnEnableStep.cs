using System;
using UnityEngine;

namespace Game.Step.LvHamb2
{
    public sealed class CompleteLevelOnEnableStep : LevelStep
    {
        private void OnEnable()
        {
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
