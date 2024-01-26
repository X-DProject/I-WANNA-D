using System.Collections;
using System.Security.Cryptography;
using TMPro;
using Tool.Module.Message;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    private float currentTime;

    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        GameInstance.Connect("countdown.begin", OnCountdown);
    }

    private void OnDisable()
    {
        GameInstance.Disconnect("countdown.begin", OnCountdown);
    }

    private void OnCountdown(IMessage msg)
    {
        var time = (float)msg.Data;
        currentTime = time;
        UpdateCountdownText();
        StartCoroutine(StartCountdown(time));
    }

    IEnumerator StartCountdown(float time)
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1.0f);  // 每隔1秒执行一次

            currentTime -= 1.0f;
            UpdateCountdownText();
        }

        GameInstance.Signal("countdown.end", time);
    }

    void UpdateCountdownText()
    {
        // 更新UI文本显示
        if (timeText != null)
        {
            timeText.text = "Time: " + Mathf.Round(currentTime).ToString();
        }
    }
}