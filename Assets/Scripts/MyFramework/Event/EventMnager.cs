using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventNull
{

}
public class Event<T>: EventNull
{
    public UnityAction<T> action;
    public Event(UnityAction<T> action)
    {
        this.action = action;
    }
}
public class Event : EventNull
{
    public UnityAction action;
    public Event(UnityAction action)
    {
        this.action = action;
    }
}
/// <summary>
/// 如果在事件中心一开始就定了类型 那字典的VALUE就全是那个类型了  这样就不对了
/// 要在添加移除监听 触发事件的时候  去定某个事件自己的类型
/// </summary>
public class EventMnager : Singleton<EventMnager>
{
    private Dictionary<string, EventNull> eventDic = new Dictionary<string, EventNull>();

    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as Event<T>).action += action;
        }
        else
        {
            eventDic.Add(name, new Event<T>(action)); 
        }
    }
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as Event).action += action;
        }
        else
        {
            eventDic.Add(name, new Event(action));
        }
    }
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as Event<T>).action -= action;
        }
    }
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as Event).action -= action;
        }
    }
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as Event<T>).action?.Invoke(info);
        }
    }
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as Event).action?.Invoke();
        }
    }
    public void Clear()
    {
        eventDic.Clear();
    }
}
