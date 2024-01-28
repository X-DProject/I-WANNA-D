using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behav;
using Tool.Module.Message;
using UnityEngine;

public class StargazerController : MonoBehaviour
{
    private AnimationPlayer anim;
    private void Awake()
    {
        anim = GetComponent<AnimationPlayer>();
        anim.SetEmoji(0.2f);
    }

    private void OnEnable()
    {
        GameInstance.Connect("one_star", OnStarShow);
        GameInstance.Connect("group_star", OnGroupStarShow);
    }

    private void OnDisable()
    {
        GameInstance.Disconnect("one_star", OnStarShow);
        GameInstance.Connect("group_star", OnGroupStarShow);
    }

    private void OnStarShow(IMessage rMessage)
    {
        anim.SetEmoji(0.5f, 1f);
        anim.SetTrigger("jump");
        StartCoroutine(Look());
    }

    private IEnumerator Look()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("look");
    }

    private void OnGroupStarShow(IMessage rMessage)
    {
        anim.SetEmoji(1f, 1f);
    }
}
