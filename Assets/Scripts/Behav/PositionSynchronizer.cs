using UnityEngine;

namespace Game.Behav
{
    public sealed class PositionSynchronizer : MonoBehaviour
    {
        [SerializeField]
        private Transform _syncTarget;

        [SerializeField]
        private bool _abslutelySync = false;

        private Vector3 _different;

        private void Start()
        {
            if (_syncTarget == null)
                throw new MissingComponentException("[PositionSynchronizer] require syncTarget.");

            _different = _syncTarget.position - transform.position;
        }

        private void FixedUpdate()
        {
            transform.position = _abslutelySync
                ? _syncTarget.position
                : _syncTarget.position - _different;
        }
    }
}


