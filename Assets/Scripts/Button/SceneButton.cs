using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneButton : MonoBehaviour
{
    public AssetReference loadScene;
    private Button button; 

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        EventHandler.CallLoadSceneEvent(loadScene);
    }

}
