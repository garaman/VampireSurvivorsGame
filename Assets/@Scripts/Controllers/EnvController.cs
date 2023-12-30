using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : BaseController
{
    Define.EnvType envType;
    public Define.EnvType EnvType { get { return envType; } protected set { envType = value; } }
    public override bool Init()
    {
        base.Init();
        EnvType = Define.EnvType.None;
        return true;
    }
}