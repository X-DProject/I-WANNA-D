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

        [SerializeField]
        private bool _debug;
        
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
            if (_debug)
                Debug.Log($"[PositionMatcher] detected {collision.gameObject.name} enter {name}.");

            if (collision.TryGetComponent(out PositionMatcher matcher))
                if (matcher.Id == this.Id && !_matchers.Contains(matcher))
                {
                    _matchers.Add(matcher);

                    if (_debug)
                        Debug.Log($"[PositionMatcher] {collision.gameObject.name} already added to {name}.");
                }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_debug)
                Debug.Log($"[PositionMatcher] detected {collision.gameObject.name} exit of {name}.");

            if (collision.TryGetComponent(out PositionMatcher matcher))
                if (matcher.Id == this.Id && !_matchers.Contains(matcher))
                {
                    _matchers.Remove(matcher);

                    if (_debug)
                        Debug.Log($"[PositionMatcher] {collision.gameObject.name} already delete from {name}.");
                }
        }

        public int  Id       => _id;
        public bool HasMatch
        {
            get
            {
                if (_debug)
                    Debug.Log($"[PositionMatcher] has {_matchers.Count} matches.");
                return _matchers.Count > 0;
            }
        }
    }
}
