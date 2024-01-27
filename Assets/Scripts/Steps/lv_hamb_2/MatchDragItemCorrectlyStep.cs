using Game.Behav;
using UnityEngine;

namespace Game.Step.LvHamb2
{
    public sealed class MatchDragItemCorrectlyStep : LevelStep
    {
        [Header("Behav")]

        [SerializeField]
        private PositionMatcher _matchArea;

        [SerializeField]
        private DraggableObject _dragItem;

        protected override sealed bool TryPassStep()
        {
            if (_matchArea == null)
                throw new MissingComponentException();

            if (_matchArea.HasMatch)
            {
                _dragItem.transform.position = _matchArea.transform.position;
                _dragItem.enabled = false;
                return true;
            }
            return false;
        }
    }
}
