using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Salesperson : MonoBehaviour
{
    float countdownTime = 3f;
    public GameObject dialogue;
    private bool isColl = false;
    private void OnEnable()
    {
        GameInstance.Connect("countdown.end", OnCountdown);
    }

    private void OnDisable()
    {
        GameInstance.Disconnect("countdown.end", OnCountdown);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            // anim
            dialogue.SetActive(true);
            GameInstance.Signal("countdown.begin", countdownTime);
            isColl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            isColl = false;
            dialogue.SetActive(false);
        }
    }

    private void OnCountdown(IMessage msg)
    {
        var time = (float)msg.Data;
        if(time != countdownTime) return;

        if(isColl)
        {
            // 
            GameInstance.Signal("move.ban");
            // game over
            Debug.Log("game over");
            GameInstance.Signal("game.over");
        }
    }


}
