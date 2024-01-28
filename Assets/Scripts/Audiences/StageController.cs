using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public List<string> CrosstalkList;
    public float talkSpacing = 1f;
    private GameObject leftCrosstalk;
    private GameObject leftBubbleImage;
    private TextMeshPro leftBubbleText;
    private GameObject rightCrosstalk;
    private GameObject rightBubbleImage;
    private TextMeshPro rightBubbleText;
    private Transform microphone;

    private void Awake()
    {
        leftCrosstalk = transform.Find("CrosstalkLeft").gameObject;
        leftBubbleImage = transform.Find("BubbleLeft/Image").gameObject;
        leftBubbleText = transform.Find("BubbleLeft/Text").GetComponent<TextMeshPro>();
        rightCrosstalk = transform.Find("CrosstalkRight").gameObject;
        rightBubbleImage = transform.Find("BubbleRight/Image").gameObject;
        rightBubbleText = transform.Find("BubbleRight/Text").GetComponent<TextMeshPro>();
        microphone = transform.Find("Microphone").transform;

        StartCoroutine(StartCrosstalk());
    }

    private IEnumerator StartCrosstalk()
    {
        for(int i = 0; i < CrosstalkList.Count; i++)
        {
            if(i%2 == 0)
            {
                microphone.localScale = new Vector3(1, 1, 1);
                BubbleChange(true, CrosstalkList[i]);
            }
            else
            {
                microphone.localScale = new Vector3(-1, 1, 1);
                BubbleChange(false, CrosstalkList[i]);
            }
            yield return new WaitForSeconds(talkSpacing);
        }

        leftBubbleImage.gameObject.SetActive(true);
        leftBubbleText.text = "...";
        leftBubbleText.gameObject.SetActive(true);
        rightBubbleImage.gameObject.SetActive(true);
        rightBubbleText.text = "...";
        rightBubbleText.gameObject.SetActive(true);

        yield break;
    }

    private void BubbleChange(bool isRight, string text)
    {
        if(isRight)
        {
            leftBubbleImage.gameObject.SetActive(false);
            leftBubbleText.gameObject.SetActive(false);
            rightBubbleImage.gameObject.SetActive(true);
            rightBubbleText.text = text;
            rightBubbleText.gameObject.SetActive(true);
        }
        else
        {
            rightBubbleImage.gameObject.SetActive(false);
            rightBubbleText.gameObject.SetActive(false);
            leftBubbleImage.gameObject.SetActive(true);
            leftBubbleText.text = text;
            leftBubbleText.gameObject.SetActive(true);
        }
    }
}
