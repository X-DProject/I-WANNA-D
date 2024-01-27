using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LaugherController : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> HaList;
    private int haIdx = 0;
    private int activeCount = 0;

    public float hideTime = 3f;
    private Color showColor = new Color(1 ,1 ,1 ,1);
    private Color hideColor = new Color(1 ,1 ,1 ,0);

    private void Awake()
    {
        foreach(var ha in HaList)
        {
            ha.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(activeCount == 8)
            {
                StartCoroutine(ShowHa(8));
                StartCoroutine(NextLevel());
            }
            else if(haIdx == 8 )
            {
                 haIdx = 0;
            }   
            else
            {
                StartCoroutine(ShowHa(haIdx));
                haIdx ++;
            }
        }
    }

    private IEnumerator ShowHa(int haIdx)
    {
        var haSprite = HaList[haIdx].transform.Find("Image").GetComponent<SpriteRenderer>();
        haSprite.color = showColor;
        HaList[haIdx].SetActive(true);
        activeCount ++;
        haSprite.DOColor(hideColor, hideTime);
        yield return new WaitForSeconds(hideTime);

        HaList[haIdx].SetActive(false);
        activeCount --;
        yield break;
    }

    private IEnumerator NextLevel()
    {
        // anim
        yield return new WaitForSeconds(1f);
        GameInstance.Signal("next_level");
        yield break;
    }


}
