using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
       return Util.GetOrAddComponent<T>(go);
    }

    public static bool IsVaild(this GameObject go)
    {
        return go != null && go.activeSelf;
    }

    public static bool IsVaild(this BaseController bc)
    {
        return bc != null && bc.isActiveAndEnabled;
    }

}
