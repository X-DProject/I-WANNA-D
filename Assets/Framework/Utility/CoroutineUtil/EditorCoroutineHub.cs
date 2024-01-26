#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine;

namespace Tool.Util
{
    public static class EditorCoroutineHub
    {
        private static ObjectPool<EditorCoroutine> corPool = new ObjectPool<EditorCoroutine>(8, 8);

        public static EditorCoroutine StartCoroutine(IEnumerator coroutine)
        {
            var routine = corPool.Allocate();
            EditorCoroutine.Start(routine, coroutine);
            return routine;
        }

        public static void StopCoroutine(EditorCoroutine routine)
        {
            routine.Stop();
            corPool.Recycle(routine);
        }

        public static EditorCoroutine CallNextFrame(Action action)
        {
            return StartCoroutine(CorCallNextFrame(action));
        }

        public static EditorCoroutine CallLater(Action action, float seconds, bool realtime = false)
        {
            return StartCoroutine(CorCallLater(action, seconds, realtime));
        }

        public static EditorCoroutine WaitUntil(Func<bool> condition, Action action)
        {
            return StartCoroutine(CorWaitUntil(condition, action));
        }

        public static EditorCoroutine WaitWhile(Func<bool> condition, Action action)
        {
            return StartCoroutine(CorWaitWhile(condition, action));
        }

        static IEnumerator CorCallNextFrame(Action action)
        {
            yield return null;
            action?.Invoke();
        }

        static IEnumerator CorCallLater(Action action, float seconds, bool realtime)
        {
            if (realtime)
                yield return new WaitForSecondsRealtime(seconds);
            else
                yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }

        static IEnumerator CorWaitUntil(Func<bool> condition, Action action)
        {
            while (!condition()) yield return null;
            action?.Invoke();
        }

        static IEnumerator CorWaitWhile(Func<bool> condition, Action action)
        {
            while (condition()) yield return null;
            action?.Invoke();
        }

        public static  IEnumerator CorLoop(IEnumerator coroutine, int loopCount)
        {
            for (int i = 0; i < loopCount; i++)
                yield return coroutine;
        }

        public static IEnumerator CorLoopForever(IEnumerator coroutine)
        {
            while (true) yield return coroutine;
        }

        public static IEnumerator CorCallback(IEnumerator coroutine, Action callback)
        {
            yield return coroutine;
            callback?.Invoke();
        }
    }
}
#endif