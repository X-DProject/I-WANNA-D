#if UNITY_EDITOR
using System.Collections;
using UnityEditor;

namespace Tool.Util
{
    public class EditorCoroutine
    {
        public static EditorCoroutine Start(IEnumerator _routine)
        {
            EditorCoroutine coroutine = new EditorCoroutine(_routine);
            coroutine.Start();
            coroutine.Done = false;
            return coroutine;
        }
        public static void Start(EditorCoroutine _ec, IEnumerator _routine)
        {
            _ec.routine = _routine;
            _ec.Done = false;
            _ec.Start();
        }

        IEnumerator routine;
        EditorCoroutine(IEnumerator _routine) { routine = _routine; }

        // For object pool usage
        // with no coroutine func binding
        public EditorCoroutine() { }

        void Start() { EditorApplication.update += Update; }
        void Update() { if (!routine.MoveNext()) Stop(); }
        public void Stop() { EditorApplication.update -= Update; Done = true; }
        public bool Done { get; private set; }
    }
}
#endif