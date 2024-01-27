using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance 
    {  
        get
        {
            if (instance == null)
                throw new InvalidOperationException($"try get a null instance.");
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestory()
    {
        if(instance == this)
        {
            instance = null; 
        }
    }
}
