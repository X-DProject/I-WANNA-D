using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Game.Behav;

public class LaugherController : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> HaList;
    public List<AudioClip> audioList;
    private int haIdx = 0;
    private int activeCount = 0;

    private AnimationPlayer anim;
    public float hideTime = 3f;
    private Color showColor = new Color(1 ,1 ,1 ,1);
    private Color hideColor = new Color(1 ,1 ,1 ,0);

    private bool _isWinned = false;

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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

        if(activeCount == 0 && !_isWinned)
        {
            GameInstance.Signal("ha.update", activeCount);
            anim.SetEmoji(0.5f, 0.5f);
        }
    }

    private IEnumerator ShowHa(int haIdx)
    {
        GameInstance.Signal("fx.play", audioList[Random.Range(0,audioList.Count)]);
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
        _isWinned = true;
        // anim
        foreach(var audio in audioList)
        {
            GameInstance.Signal("fx.play", audio);
        }
        
        var audiences = FindObjectsOfType<Audience>();
        foreach (var au in audiences)
        {
            yield return new WaitForSeconds(0.1f);
            var anim = au.GetComponent<AnimationPlayer>();
            anim.ChangeAnimParamLerply("stand_jump float 1 0.5");
        }

        yield return new WaitForSeconds(4f);
        GameInstance.Signal("next_level");
        
        yield break;
    }


}
