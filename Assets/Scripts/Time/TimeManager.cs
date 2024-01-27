using System.Collections;
using System.Security.Cryptography;
using TMPro;
using Tool.Module.Message;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Transform timeList;
    public GameObject timePrefab;

    private void OnEnable()
    {
        GameInstance.Connect("countdown.begin", OnCountdown);
        GameInstance.Connect("next_level", OnTimerClear);
        GameInstance.Connect("game.over", OnTimerClear);
    }

    private void OnDisable()
    {
        GameInstance.Disconnect("countdown.begin", OnCountdown);
        GameInstance.Disconnect("next_level", OnTimerClear);
        GameInstance.Disconnect("game.over", OnTimerClear);
    }

    private void OnCountdown(IMessage msg)
    {
        var time = (float)msg.Data;
        if (timePrefab != null)
        {
            GameObject instantiatedPrefab = Instantiate(timePrefab, transform.position, Quaternion.identity);
            instantiatedPrefab.transform.SetParent(timeList);

            instantiatedPrefab.GetComponent<TimeCountdown>().Begin(time);
        }
    }

    private void OnTimerClear(IMessage msg)
    {
        foreach (Transform child in timeList.transform)
        {
            Destroy(child.gameObject);
        }
    }
}