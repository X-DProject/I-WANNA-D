using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void Awake()
    {
        GameInstance.Connect("next_level", OnNextLevel);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameInstance.Disconnect("next_level", OnNextLevel);
    }

    private void OnNextLevel(IMessage msg)
    {
        this.gameObject.SetActive(true);
    }

    public void HideView()
    {
        this.gameObject.SetActive(false);
    }
}
