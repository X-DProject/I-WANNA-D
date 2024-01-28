using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Game.Behav;

public class LaugherController : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> HaList;
    private int haIdx = 0;
    private int activeCount = 0;

    private AnimationPlayer anim;
    public float hideTime = 3f;
    private Color showColor = new Color(1 ,1 ,1 ,1);
    private Color hideColor = new Color(1 ,1 ,1 ,0);

    private void Awake()
    {
        foreach(var ha in HaList)
        {
            ha.gameObject.SetActive(false);
        }
        anim = GetComponent<AnimationPlayer>();
        anim.ChangeAnimParamDirectly("happy_angry float 0.5");
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
            GameInstance.Signal("ha.update", activeCount);
        }

        if(activeCount == 0)
        {
            GameInstance.Signal("ha.update", activeCount);
            anim.SetEmoji(0.5f, 0.5f);
        }
    }

    private IEnumerator ShowHa(int haIdx)
    {
        anim.SetEmoji(1f);
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
