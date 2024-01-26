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
                throw new MissingComponentException("[PositionMatcher] need collider2D to play a work.");

            if (!collider.isTrigger)
                Debug.LogWarning("[PositionMatcher] collider should be set as Trigger.");

            if (_id == default)
                Debug.LogWarning("[PositionMatcher] we don't suggest use default value for id.");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PositionMatcher collider))
                _matchers.Add(collider);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PositionMatcher collider))
                _matchers.Remove(collider);
        }

        public int  Id       => _id;
        public bool HasMatch => _matchers.Any(m => m.Id == this.Id);
        public GameObject[] GetMatches()
        {
            List<GameObject> matches = new(capacity: _matchers.Count);

            foreach (var matcher in _matchers)
            {
                if (matcher.Id == this.Id)
                    matches.Add(matcher.gameObject);
            }
            return matches.ToArray();
        }
    }
}
