using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public float spacing = 0.1f;
    public Image TitleImage;
    public Image PeopleImage;
    public GameObject sceneButton;
    [SerializeField]
    public List<Sprite> title;
    [SerializeField]
    public List<Sprite> level1;
    [SerializeField]
    public List<Sprite> level2;
    [SerializeField]
    public List<Sprite> level3;
    [SerializeField]
    public List<Sprite> level4;
    [SerializeField]
    public List<Sprite> level5;

    private Dictionary<int, List<Sprite>> levelMap = new();

    private void Awake()
    {
        GameInstance.Connect("next_level", OnNextLevel);
        this.gameObject.SetActive(false);

        levelMap[1] = level1;
        levelMap[2] = level2;
        levelMap[3] = level3;
        levelMap[4] = level4;
        levelMap[5] = level5;
    }

    private void OnDestroy()
    {
        GameInstance.Disconnect("next_level", OnNextLevel);
    }

    private void OnNextLevel(IMessage msg)
    {
        var nextLevel = GameInstance.Instance.currLevelIdx + 1;
        sceneButton.GetComponent<SceneButton>().loadScene = GameInstance.Instance.GetNextLevel();
        //anim
        
        TitleImage.sprite = title[nextLevel - 1];
        TitleImage.SetNativeSize();
        this.gameObject.SetActive(true);
        StartCoroutine(PlayAnim(nextLevel));
    }

    private IEnumerator PlayAnim(int nextLevel)
    {
        foreach(var image in levelMap[nextLevel])
        {
            PeopleImage.sprite = image;
            PeopleImage.SetNativeSize();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void HideView()
    {
        this.gameObject.SetActive(false);
    }
}
