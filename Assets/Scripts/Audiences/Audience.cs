using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Behav;
using Tool.Module.Message;

public class Audience : MonoBehaviour
{
    private AnimationPlayer anim;
    private float transformSpeed;
    void Awake()
    {
        anim = GetComponent<AnimationPlayer>();
        anim.SetEmoji(Random.Range(0.2f, 0.5f), Random.Range(0.1f, 1f));
    }

    private void OnEnable()
    {
        GameInstance.Connect("ha.update", OnHaUpdate);
    }

    private void OnDisable()
    {
        GameInstance.Disconnect("ha.update", OnHaUpdate);
    }

    private void OnHaUpdate(IMessage msg)
    {
        var num = (int)msg.Data;
        if(num == 0)
        {
            anim.SetEmoji(Random.Range(0.2f, 0.5f), Random.Range(0.1f, 1f));
            return;
        }
        var target = (float)num/8 * 0.5f + 0.4f + Random.Range(0f, 0.1f);
        anim.SetEmoji(target, Random.Range(0.1f, 0.3f));
    }
}
