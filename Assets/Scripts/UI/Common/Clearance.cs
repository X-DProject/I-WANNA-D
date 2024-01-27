using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool.Module.Message;

public class Clearance : MonoBehaviour
{
    public GameObject sceneButton;

    private void Awake()
    {
        GameInstance.Connect("clearance.show", OnClearance);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameInstance.Disconnect("clearance.show", OnClearance);
    }

    private void OnClearance(IMessage msg)
    {
        sceneButton.GetComponent<SceneButton>().loadScene = GameInstance.Instance.levelList[0];
        this.gameObject.SetActive(true);
    }

    public void HideView()
    {
        this.gameObject.SetActive(false);
    }
}
