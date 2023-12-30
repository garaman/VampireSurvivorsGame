using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : EnvController
{
    public override bool Init()
    {
        base.Init();
        EnvType = Define.EnvType.Gold;
        return true;
    }
}
