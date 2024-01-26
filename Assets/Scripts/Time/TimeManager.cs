using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60.0f;  // 设置倒计时的总时间
    private float currentTime;

    public Text countdownText;  // 用于显示倒计时的UI文本

    void Start()
    {
        currentTime = countdownTime;
        UpdateCountdownText();
        StartCoroutine(StartCountdown());
    }

    private void OnEnable()
    {
        // GameInstance.Connect("")
    }

    private void OnDisable()
    {

    }

    IEnumerator StartCountdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1.0f);  // 每隔1秒执行一次

            currentTime -= 1.0f;
            UpdateCountdownText();
        }

        // 倒计时结束时的处理
        Debug.Log("Countdown complete!");
    }

    void UpdateCountdownText()
    {
        // 更新UI文本显示
        if (countdownText != null)
        {
            countdownText.text = "Time: " + Mathf.Round(currentTime).ToString();
        }
    }
}