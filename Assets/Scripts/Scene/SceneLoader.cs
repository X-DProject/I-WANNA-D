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
    [SerializeField]
    private AssetReference _firstScene;

    [SerializeField]
    private float _fadeDuration;

    [SerializeField]
    private bool _dontLoadSceneOnStart;

    private AssetReference _currScene;
    private AssetReference _nextScene;

    private bool _isLoading;

    private void Start()
    {
        if (_dontLoadSceneOnStart)
            return;

        if (_firstScene == null)
        {
            Debug.LogWarning("√ª”–≈‰÷√FirstScene");
            return;
        }

        Debug.Log(_firstScene.SubObjectName + " " + SceneManager.GetActiveScene().name);
        _currScene = _firstScene;
        _currScene.LoadSceneAsync(LoadSceneMode.Additive);
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
        
        if (_isLoading)
            return;

        _isLoading = true;
        _nextScene = loadScene;

        if (_currScene != null)
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
        GameInstance.Signal("fade.out", _fadeDuration);
        yield return new WaitForSeconds(_fadeDuration);

        yield return _currScene.UnLoadScene(); 
        
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = _nextScene.LoadSceneAsync(LoadSceneMode.Additive,true);
        loadingOption.Completed += OnLoadCompleted;
    }


    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        _currScene = _nextScene;
        EventHandler.CallFadeInEvent(_fadeDuration);
        _isLoading = false;
    }

}
