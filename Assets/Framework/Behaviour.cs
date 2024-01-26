// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using Tool.Module.Message;
// using Tool.Util;

// namespace Tool
// {
//     public class ToolBehaviour : MonoBehaviour
//     {
//         #region Message
//         // Local msg support
//         public delegate void MsgListener(string msg, object data, GameObject src);
//         public event MsgListener msgListeners;

//         virtual protected void Awake() => msgListeners += OnMsg;
//         virtual protected void OnMsg(string msg, object data, GameObject src) { }
//         public void Tell(string msg, object data = null, GameObject src = null) => msgListeners(msg, data, src);
//         public void TellSimple(string msg) => msgListeners(msg, null, null); // for UnityEvents

//         // Global msg support
//         public void Signal(string msg, object data = null, object src = null, float delay = 0.0f)
//         {
//             MessageDispatcher.SendMessage(src, msg, data, delay);
//         }
//         public void Connect(string msg, MessageHandler handler)
//         {
//             MessageDispatcher.AddListener(msg, handler, true);
//         }
//         public void Disconnect(string msg, MessageHandler handler)
//         {
//             MessageDispatcher.RemoveListener(msg, handler, true);
//         }
//         #endregion

//         #region Coroutine
//         private Dictionary<string, Coroutine> corsTable = new Dictionary<string, Coroutine>();
//         private IEnumerator MakeCoroutineWarpper(string name, IEnumerator coroutine)
//         {
//             yield return coroutine;
//             corsTable.Remove(name);
//         }
//         public void DumpCoroutineTable() => Debug.Log(corsTable.Serialize());
//         public class CoAction
//         {
//             public string name;
//             public Coroutine coro;
//             private ToolBehaviour host;

//             public CoAction(string name, Coroutine coro, ToolBehaviour host)
//             {
//                 this.name = name;
//                 this.coro = coro;
//                 this.host = host;
//             }

//             public bool isBusy() => host.IsCoroutineRunning(name);
//             public void stop() => host.FindAndStopCoroutine(name);
//         }
//         public Coroutine FindCoroutine(string name) => corsTable.Get(name, null);
//         public bool IsCoroutineRunning(string name) => corsTable.ContainsKey(name);
//         public CoAction AddCoroutine(string name, IEnumerator coroutine)
//         {
//             if (!gameObject.activeInHierarchy) return null;
//             Debug.Assert(!corsTable.ContainsKey(name), $"Coroutine key \"{name}\" already exist!");
//             var unityCoroutine = StartCoroutine(MakeCoroutineWarpper(name, coroutine));
//             corsTable.Add(name, unityCoroutine);
//             return new CoAction(name, unityCoroutine, this);
//         }
//         public CoAction ReplaceCoroutine(string name, IEnumerator coroutine)
//         {
//             if (!gameObject.activeInHierarchy) return null;
//             FindAndStopCoroutine(name);
//             return AddCoroutine(name, coroutine);
//         }
//         public void FindAndStopCoroutine(string name)
//         {
//             if (corsTable.ContainsKey(name))
//             {
//                 StopCoroutine(corsTable[name]);
//                 corsTable.Remove(name);
//             }
//         }

//         public Coroutine CallLater(float delay, System.Action action)
//         {
//             return StartCoroutine(_CorCallLater(delay, action));
//         }
//         public Coroutine CallLater<T>(float delay, System.Action<T> action, T param)
//         {
//             return StartCoroutine(_CorCallLater(delay, action, param));
//         }
//         public Coroutine CallNextFrame(System.Action action)
//         {
//             return StartCoroutine(_CorCallNextFrame(action));
//         }
//         public Coroutine CallNextFrame<T>(System.Action<T> action, T param)
//         {
//             return StartCoroutine(_CorCallNextFrame(action, param));
//         }
//         public Coroutine CallAfterFrames(int frames, System.Action action)
//         {
//             return StartCoroutine(_CorCallAfterFrames(frames, action));
//         }
//         public Coroutine CallAfterFrames<T>(int frames, System.Action<T> action, T param)
//         {
//             return StartCoroutine(_CorCallAfterFrames(frames, action, param));
//         }
//         public Coroutine CallEndOfFrame(System.Action action)
//         {
//             return StartCoroutine(_CorCallEndOfFrame(action));
//         }
//         public Coroutine CallEndOfFrame<T>(System.Action<T> action, T param)
//         {
//             return StartCoroutine(_CorCallEndOfFrame(action, param));
//         }
//         protected IEnumerator _CorCallLater(float delay, System.Action action)
//         {
//             yield return new WaitForSeconds(delay);
//             action?.Invoke();
//         }
//         protected IEnumerator _CorCallLater<T>(float delay, System.Action<T> action, T param)
//         {
//             yield return new WaitForSeconds(delay);
//             action?.Invoke(param);
//         }
//         protected IEnumerator _CorCallNextFrame(System.Action action)
//         {
//             yield return null;
//             action?.Invoke();
//         }
//         protected IEnumerator _CorCallNextFrame<T>(System.Action<T> action, T param)
//         {
//             yield return null;
//             action?.Invoke(param);
//         }
//         protected IEnumerator _CorCallAfterFrames(int frames, System.Action action)
//         {
//             for (int i = 0; i < frames; i++) yield return null;
//             action?.Invoke();
//         }
//         protected IEnumerator _CorCallAfterFrames<T>(int frames, System.Action<T> action, T param)
//         {
//             for (int i = 0; i < frames; i++) yield return null;
//             action?.Invoke(param);
//         }
//         protected IEnumerator _CorCallEndOfFrame(System.Action action)
//         {
//             yield return new WaitForEndOfFrame();
//             action?.Invoke();
//         }
//         protected IEnumerator _CorCallEndOfFrame<T>(System.Action<T> action, T param)
//         {
//             yield return new WaitForEndOfFrame();
//             action?.Invoke(param);
//         }
//         #endregion

//         #region Log
//         public void print(params object[] list)
//         {
//             #region Debug
//             if (list == null || list.Length == 0) return;
//             string content = list[0]?.ToString();
//             string tab = "   ";
//             for (int i = 1; i < list.Length; i++) content += tab + list[i];
//             Debug.Log(content);
//             #endregion
//         }
//         public void error(params object[] list)
//         {
//             #region Debug
//             if (list == null || list.Length == 0) return;
//             string content = list[0].ToString();
//             string tab = "   ";
//             for (int i = 1; i < list.Length; i++) content += tab + list[i];
//             Debug.LogError(content);
//             #endregion
//         }
//         public void warning(params object[] list)
//         {
//             #region Debug
//             if (list == null || list.Length == 0) return;
//             string content = list[0].ToString();
//             string tab = "   ";
//             for (int i = 1; i < list.Length; i++) content += tab + list[i];
//             Debug.LogWarning(content);
//             #endregion
//         }
//         #endregion
//     }
// }