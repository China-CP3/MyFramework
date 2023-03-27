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
/// ������¼�����һ��ʼ�Ͷ������� ���ֵ��VALUE��ȫ���Ǹ�������  �����Ͳ�����
/// Ҫ������Ƴ����� �����¼���ʱ��  ȥ��ĳ���¼��Լ�������
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
