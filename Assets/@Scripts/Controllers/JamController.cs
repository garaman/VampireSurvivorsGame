using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamController : EnvController
{
    public override bool Init()
    {
        base.Init();
        EnvType = Define.EnvType.Jam;
        return true;
    }
}
