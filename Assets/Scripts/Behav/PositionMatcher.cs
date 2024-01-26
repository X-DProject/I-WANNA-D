using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Behav
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class PositionMatcher : MonoBehaviour
    {
        [SerializeField]
        private int _id;
        
        private readonly List<PositionMatcher> _matchers = new(capacity: 99);

        private void Awake()
        {
            if (!TryGetComponent(out Collider2D collider))
                throw new MissingComponentException($"[PositionMatcher] {name} need collider2D to play a work.");

            if (!collider.isTrigger)
                Debug.LogWarning($"[PositionMatcher] on {name}, collider should be set as Trigger.");

            if (_id == default)
                Debug.LogWarning($"[PositionMatcher] on {name}, we don't suggest use default value for id.");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PositionMatcher matcher))
                if (matcher.Id == this.Id)
                    _matchers.Add(matcher);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PositionMatcher matcher))
                if (matcher.Id == this.Id)
                    _matchers.Remove(matcher);
        }

        public int  Id       => _id;
        public bool HasMatch => _matchers.Count > 0;
    }
}
