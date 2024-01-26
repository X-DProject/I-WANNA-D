using Game.Behav;
using UnityEngine;

namespace Game.Step.LvHamb2
{
    public sealed class MatchDragItemCorrectlyStep : LevelStep
    {
        [SerializeField]
        private PositionMatcher _matchArea;

        [SerializeField]
        private DraggableObject _dragItem;

        public void Check()
        {
            if (_matchArea == null)
                throw new MissingComponentException();

            if (_matchArea.HasMatch)
            {
                this.IsOk = true;

                _dragItem.transform.position = _matchArea.transform.position;
                _dragItem.enabled = false;
            }
        }
    }
}
