using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TimeCountdown : MonoBehaviour
{
    private float currentTime;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    public void Begin(float time)
    {
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
        Destroy(this.gameObject);
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
