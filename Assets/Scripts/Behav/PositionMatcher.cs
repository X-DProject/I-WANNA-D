using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Behav
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class PositionMatcher : MonoBehaviour
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private UnityEvent _onMatched;

        [SerializeField]
        private UnityEvent _onDismatched;

        [SerializeField]
        private bool _debug;

        private readonly List<PositionMatcher> _matchers = new(capacity: 99);

        private void Awake()
        {
            if (!TryGetComponent(out Collider2D _))
                throw new MissingComponentException($"[PositionMatcher] {name} need collider2D to play a work.");

            if (_id == default)
                Debug.LogWarning($"[PositionMatcher] on {name}, we don't suggest use default value for id.");
        }

        private void OnTriggerEnter2D(Collider2D collision) => MatchOnEnter(collision.gameObject);
        private void OnTriggerExit2D(Collider2D collision) => MatchOnExit(collision.gameObject);
        private void OnCollisionEnter2D(Collision2D collision) => MatchOnEnter(collision.gameObject);
        private void OnCollisionExit2D(Collision2D collision) => MatchOnExit(collision.gameObject);

        private void MatchOnEnter(GameObject obj)
        {
            if (_debug)
                Debug.Log($"[PositionMatcher] detected {obj.name} enter {name}.");

            if (obj.TryGetComponent(out PositionMatcher matcher))
                if (matcher.Id == this.Id && !_matchers.Contains(matcher))
                {
                    _matchers.Add(matcher);
                    _onMatched?.Invoke();

                    if (_debug)
                        Debug.Log($"[PositionMatcher] {obj.name} already added to {name}.");
                }
        }
        private void MatchOnExit(GameObject obj)
        {
            if (_debug)
                Debug.Log($"[PositionMatcher] detected {obj.name} exit of {name}.");

            if (obj.TryGetComponent(out PositionMatcher matcher))
                if (matcher.Id == this.Id && _matchers.Contains(matcher))
                {
                    _matchers.Remove(matcher);
                    _onDismatched?.Invoke();

                    if (_debug)
                        Debug.Log($"[PositionMatcher] {obj.name} already delete from {name}.");
                }
        }

        public int Id => _id;
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
