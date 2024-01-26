using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public AssetReference firstScene;

    private AssetReference currScene;
    private AssetReference nextScene;
    private Vector3 positionToGo;
    private bool isLoading;
    public float fadeDuration;

    private void Start()
    {
        currScene = firstScene;
        currScene.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        EventHandler.LoadSceneEvent += OnLoadSceneEvent;
    }

    private void OnDisable()
    {
        EventHandler.LoadSceneEvent -= OnLoadSceneEvent;
    }

    private void OnLoadSceneEvent(AssetReference loadScene)
    {
        
        if (isLoading)
            return;

        isLoading = true;
        nextScene = loadScene;

        if (currScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            LoadNewScene();
        }
    }

    private IEnumerator UnLoadPreviousScene()
    {
        // EventHandler.CallFadeOutEvent(fadeDuration);
        GameInstance.Signal("fade.out", fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        yield return currScene.UnLoadScene(); 
        
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = nextScene.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed += OnLoadCompleted;
    }


    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currScene = nextScene;
        EventHandler.CallFadeInEvent(fadeDuration);
        isLoading = false;
    }

}
