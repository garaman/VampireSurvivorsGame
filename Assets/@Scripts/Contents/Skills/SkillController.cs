using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EgoSword : ��Ÿ
// FireProjectile : ����ü
// PoisonField : ���� 

public class SkillController : BaseController
{
    public Define.SkillType SkillType { get; set; }
    public DataXml.SkillData SkillData { get; protected set; }

    #region Destroy
    Coroutine _coDestroy;

    public void StartDestroy(float delaySeconds)
    {
        StopDestroy();
        _coDestroy = StartCoroutine(CoDestroy(delaySeconds));
    }

    public void StopDestroy()
    {
        if(_coDestroy != null) 
        { 
            StopCoroutine(_coDestroy);
            _coDestroy = null;
        }
    }

    IEnumerator CoDestroy(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        if(this.IsVaild())
        {
            Managers.Object.Despawn(this);
        }
    }
    #endregion
}
