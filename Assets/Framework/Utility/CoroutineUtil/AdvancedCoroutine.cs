using System;
using System.Collections;
using UnityEngine;

namespace Tool.Util
{
    public class AdvancedCoroutine
    {
        public bool Done { get; private set; }

        private IEnumerator raw;
        private Action<bool> onfinish;
        private Coroutine unityCoroutine;

        public AdvancedCoroutine(IEnumerator raw) => InitCoroutine(raw);
        public AdvancedCoroutine(IEnumerator raw, Action<bool> onfinish) => InitCoroutine(raw, onfinish);

        private void InitCoroutine(IEnumerator raw, Action<bool> onfinish = null)
        {
            Done = false;
            this.raw = raw;
            this.onfinish = onfinish;
        }
        private IEnumerator CoroutineWrapper(IEnumerator raw, Action<bool> onfinish)
        {
            yield return raw;
            onfinish?.Invoke(true);
            Done = true;
        }

        public void Start(MonoBehaviour host)
        {
            unityCoroutine = host.StartCoroutine(CoroutineWrapper(raw, onfinish));
        }
        public void Stop(MonoBehaviour host)
        {
            if (Done) return;
            host.StopCoroutine(unityCoroutine);
            onfinish?.Invoke(false);
        }

    }

    public static class CoroutineExtension
    {
        public static AdvancedCoroutine StartAdvancedCoroutine(this MonoBehaviour host, IEnumerator raw, Action<bool> onfinish = null)
        {
            var cor = new AdvancedCoroutine(raw, onfinish);
            cor.Start(host);
            return cor;
        }
        public static void StopAdvancedCoroutine(this MonoBehaviour host, AdvancedCoroutine cor) => cor.Stop(host);
    }
}