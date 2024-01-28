using System;
using System.Collections;
using System.Collections.Generic;
using Tool.Module.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
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

    private void Awake()
    {
        GameInstance.Connect("next_level", OnNextLevel);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameInstance.Disconnect("next_level", OnNextLevel);
    }

    private void OnNextLevel(IMessage msg)
    {
        sceneButton.GetComponent<SceneButton>().loadScene = GameInstance.Instance.GetNextLevel();
        this.gameObject.SetActive(true);
    }

    public void HideView()
    {
        this.gameObject.SetActive(false);
    }
}
