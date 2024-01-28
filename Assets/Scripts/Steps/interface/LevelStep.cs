using UnityEngine;
using UnityEngine.Events;

namespace Game.Step
{
    public abstract class LevelStep : MonoBehaviour
    {
        [Header("Process")]

        [SerializeField]
        private bool _isFinished = false;

        [SerializeField]
        private LevelStep _nextStep;

        [SerializeField]
        private UnityEvent _onFinished;

        private bool Enable
        {
            get => this.enabled && gameObject.activeInHierarchy;
            set
            {
                this.enabled = value;
                gameObject.SetActive(value);
            }
        }
        
        public bool IsFinished 
            => _isFinished;
        public void Check() // call on UnityEvent
        {
            if (!this.Enable)
                return;

            if (TryPassStep())
            {            
                this._isFinished = true;
                this.Enable = false;

                _onFinished?.Invoke();

                Debug.Log($"[Step] step {name} is finished.");

                if (_nextStep != null)
                    _nextStep.Enable = true;
            }
        } 
        protected abstract bool TryPassStep();
    }
}
