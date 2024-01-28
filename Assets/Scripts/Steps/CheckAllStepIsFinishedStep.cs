using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Step
{
    public class CheckAllStepIsFinishedStep : LevelStep
    {
        [Header("Behav")]

        [SerializeField]
        private LevelStep[] _steps;

        protected override bool TryPassStep()
        {
            return _steps.All(s => s.IsFinished);
        }
    }
}


