using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tool.Module.Message;
using UnityEngine;
using System.Linq;

enum Stage
{
    Begin,
    Half,
}

public class BirdGroup : MonoBehaviour
{
    public float spacing = 1;
    private List<int> ToneList = new();
    private Stage stage = Stage.Begin;
    public List<Transform> birdList = new();

    public GameObject star;
    public GameObject starGroup;

    private List<int> firstToneList = new List<int>
    {
        1, 1, 5, 5, 6, 6, 5
    };
    private List<int> secondToneList = new List<int>
    {
        4, 4, 3, 3, 2, 2, 1
    };

    private void Start()
    {
        star.SetActive(false);
        starGroup.SetActive(false);
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
        case Stage.Begin:
            if(ToneList[index] != firstToneList[index]) ToneList.Clear();
            break;
        case Stage.Half:
            if(ToneList[index] != secondToneList[index]) ToneList.Clear();
            break;
        }
        Compare();
        if(ToneList.Count >= 7) ToneList.Clear();
    }
    private void Compare()
    {
        if (stage == Stage.Begin && ToneList.SequenceEqual(firstToneList))
        {
            Debug.Log("stage change");
            star.SetActive(true);
            stage = Stage.Half;
        }

        if (stage == Stage.Half && ToneList.SequenceEqual(secondToneList))
        {
            starGroup.SetActive(true);
            // Clearance
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
