using Game.Behav;
using UnityEngine;

namespace Game.Step.LvHamb2
{
    public sealed class MatchWordCorrectlyStep : LevelStep
    {
        [SerializeField]
        private PositionMatcher _angryTextMatchArea;

        [SerializeField]
        private DraggableObject _hunText;

        public void Check()
        {
            if (_angryTextMatchArea == null)
                throw new MissingComponentException();

            if (_angryTextMatchArea.HasMatch)
            {
                this.IsOk = true;

                _hunText.transform.position = _angryTextMatchArea.transform.position;
                _hunText.enabled = false;
            }
        }
    }
}
