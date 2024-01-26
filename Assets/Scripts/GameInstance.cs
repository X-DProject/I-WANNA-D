using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using Tool.Util;
using Tool.Module.Message;

public class GameInstance : Singleton<GameInstance>
{
    private void Awake()
    {
        MessageDispatcher.Init(gameObject);
    }
}
