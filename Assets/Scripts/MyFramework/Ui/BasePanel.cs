using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Toggle = UnityEngine.UI.Toggle;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class BasePanel : MonoBehaviour
{
    protected Dictionary<string, List<UIBehaviour>> controDic = new Dictionary<string, List<UIBehaviour>>();
    protected virtual void Awake()
    {
        FindUiContro<Button>();
        FindUiContro<Image>();
        FindUiContro<Toggle>();
        FindUiContro<Slider>();
        FindUiContro<ScrollRect>();
        FindUiContro<Text>();
        FindUiContro<GridLayoutGroup>();
    }
    private void FindUiContro<T>() where T:UIBehaviour
    {
        T[] s = GetComponentsInChildren<T>();
        for (int i = 0; i < s.Length; i++)
        {
           string name = s[i].gameObject.name;
           if(controDic.ContainsKey(name))
           {
               controDic[name].Add(s[i]);
           }
           else
           {
               controDic.Add(name, new List<UIBehaviour>() { s[i] });
           }
           if (s[i] is Button)
           {
               (s[i] as Button).onClick.AddListener(() =>
               {
                   OnClick(name);
               });
           }
           else if (s[i] is Toggle)
           {
               (s[i] as Toggle).onValueChanged.AddListener((b) =>
               {
                   onValueChanged(name, b);
               });
           }
        }
    }
    public T GetUiContro<T>(string name) where T : UIBehaviour
    {
        if(controDic.ContainsKey(name))
        {
            for (int i = 0; i < controDic[name].Count; i++)
            {
                if (controDic[name][i] is T)
                {
                    return controDic[name][i] as T;
                }
            }
        }
        return null;
    }
    public virtual void ShowSelf()
    {

    }
    public virtual void HideSelf()
    {

    }
    protected virtual void OnClick(string btnName)
    {

    }
    protected virtual void onValueChanged(string toggleName, bool b)
    {

    }
}
