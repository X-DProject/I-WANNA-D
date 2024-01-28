using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Game.Behav;

public class Salesperson : MonoBehaviour
{
    float countdownTime = 3f;
    public GameObject dialogue;
    private bool isColl = false;
    private AnimationPlayer anim;

    private void Awake()
    {
        anim = GetComponent<AnimationPlayer>();
        anim.SetEmoji(0.5f);
    }

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
            anim.SetEmoji(0, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            isColl = false;
            dialogue.SetActive(false);
            anim.SetEmoji(0.5f, 1);
        }
    }

    private void OnCountdown(IMessage msg)
    {
        var time = (float)msg.Data;
        if(time != countdownTime) return;

        if(isColl)
        {
            GameInstance.Signal("move.ban");
            GameInstance.Signal("game.over");
        }
    }

}
