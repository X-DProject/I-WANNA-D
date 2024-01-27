using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tool.Module.Message;
using UnityEngine;
using System.Linq;

enum Stage
{
    Half,
    Finish
}

public class BirdGroup : MonoBehaviour
{
    public float spacing = 1;
    private List<int> ToneList = new();
    private int stage = 0;
    public List<Transform> birdList = new();
    private List<int> rightList = new List<int>
    {
        1, 1, 5, 5, 6, 6, 5
    };
    private List<int> rightList2 = new List<int>
    {
        4, 4, 3, 3, 2, 2, 1
    };

    private void Start()
    {
        SetChirping(false);
        StartCoroutine(AwakeSing());
    }

    private void OnEnable()
    {
        GameInstance.Connect("tone.update", OnToneUpdate);
    }

    private void OnDisable()
    {
        GameInstance.Disconnect("tone.update", OnToneUpdate);
    }

    private void OnToneUpdate(IMessage mag)
    {
        var tone = (int)mag.Data;
        var index = ToneList.Count;
        ToneList.Add(tone);
        switch (stage)
        {
        case 0:
            if(ToneList[index] != rightList[index]) ToneList.Clear();
            break;
        case 1:
            if(ToneList[index] != rightList2[index]) ToneList.Clear();
            break;
        }
        Compare();
        if(ToneList.Count >= 7) ToneList.Clear();
    }
    private void Compare()
    {
        if (stage == 0 && ToneList.SequenceEqual(rightList))
        {
            Debug.Log("stage change");
            stage = 1;
        }

        if (stage == 1 && ToneList.SequenceEqual(rightList2))
        {
            // next level
            GameInstance.Signal("clearance.show");
        }
    }

    private IEnumerator AwakeSing()
    {
        yield return new WaitForSeconds(1f);
        foreach( var bird in birdList)
        {
            bird.GetComponent<Bird>().Chirping(); 
            yield return new WaitForSeconds(spacing);
        }
        SetChirping(true);
        yield break;
    }

    private void SetChirping(bool canChirping)
    {
        foreach( Transform bird in birdList)
        {
            bird.GetComponent<Bird>().canChirping = canChirping;
        }
    }


}
