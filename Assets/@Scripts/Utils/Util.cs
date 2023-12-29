using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if(component == null) { component = go.AddComponent<T>(); }
        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursice = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursice);
        if(transform == null)
        {
            return null;
        }
        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursice = false) where T : UnityEngine.Object
    {
        if(go == null) return null;

        if(recursice==false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform  = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T compoenet = transform.GetComponent<T>();
                    if(compoenet != null)
                    {
                        return compoenet;
                    }
                }
            }
            
        }
        else
        {
            foreach (T compoenet in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || compoenet.name == name)
                {
                    return compoenet;
                }
            }
        }

        return null;
    }
}
