using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool.Module.Message;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
        GameInstance.Connect("game.over", OnGameOver);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameInstance.Disconnect("game.over", OnGameOver);
    }

    private void OnGameOver(IMessage msg)
    {
        this.gameObject.SetActive(true);
    }

    public void HideView()
    {
        this.gameObject.SetActive(false);
    }
}
