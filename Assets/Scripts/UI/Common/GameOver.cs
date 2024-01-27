using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool.Module.Message;

public class GameOver : MonoBehaviour
{
    public GameObject sceneButton;
    private void Awake()
    {
        GameInstance.Connect("game.over", OnGameOver);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameInstance.Disconnect("game.over", OnGameOver);
    }

    private void OnGameOver(IMessage msg)
    {
        if (sceneButton == null)
            throw new MissingComponentException("sceneButton gameObject is null.");

        if (!sceneButton.TryGetComponent(out SceneButton sceneBtn))
            throw new MissingComponentException("sceneButton component is missing!");
            
        var currLv = GameInstance.Instance.GetCurrLevel();
        sceneBtn.loadScene = currLv;
        
        this.gameObject.SetActive(true);
    }

    public void HideView()
    {
        this.gameObject.SetActive(false);
    }
}
