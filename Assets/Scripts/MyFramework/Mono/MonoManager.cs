using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : SingletonMono<MonoManager>
{
    private UnityAction action;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void AddListner(UnityAction action)
    {
        this.action += action;
    }
    public void RemoveListner(UnityAction action)
    {
        this.action -= action;
    }
    public void Clear()
    {
        action = null;
    }
    private void Update()
    {
        action?.Invoke();
    }
}
