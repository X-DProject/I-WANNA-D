using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public List<string> CrosstalkList;

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

        
    }

    private IEnumerator StartCrosstalk()
    {
        yield break;
    }
}
