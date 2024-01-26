using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class TailOfQueue : MonoBehaviour
{
    public float countdownTime = 15f;
    public GameObject queue;

    public float moveDuration = 3f;

    private void Awake()
    {
        GameInstance.Connect("countdown.end", OnCountdownEnd);
    }

    private void Start()
    {
        GameInstance.Signal("countdown.begin", countdownTime);
    }

    private void OnDestroy()
    {
        GameInstance.Disconnect("countdown.end", OnCountdownEnd);
    }

    private void OnCountdownEnd(IMessage msg)
    {
        if(countdownTime != (float)msg.Data) return;
        // anim
        StartCoroutine(PlayQueueAnim());
    }

    private IEnumerator PlayQueueAnim()
    {
        queue.transform.DOMoveX(-2f, moveDuration);
        yield return new WaitForSeconds(moveDuration);
        // trigger
        this.transform.DOMoveX(-7f, 0.1f);
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            // ban input 
            GameInstance.Signal("move.ban");
            // anim
            
            // next level
            GameInstance.Signal("next_level");
        }
    }

}
