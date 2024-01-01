using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// EgoSword : ��Ÿ
// FireProjectile : ����ü
// PoisonField : ���� 

public class SkillBase : BaseController
{
    public CreatureController Owner { get; set; }
    public Define.SkillType SkillType { get; set; } = Define.SkillType.None;
    public DataXml.SkillData SkillData { get; protected set; }

    public int SkillLevel 
    { 
        get { return SkillData.level; } 
        set { SkillData.level = value; } 
    }

    public bool IsLearnedSkill { get { return SkillLevel > 0; } }

    public int Damage { get; set; } = 1000;

    public SkillBase(Define.SkillType skillType)
    {
        SkillType = skillType;
    }

    public virtual void ActivateSkill()
    {

    }

    protected virtual void GenerateProjectile(int templateID, CreatureController owner, Vector3 startPos, Vector3 dir, Vector3 targetPos)
    {
        ProjectileController pc = Managers.Object.Spawn<ProjectileController>(startPos, templateID);
        pc.SetInfo(templateID, owner, dir);
    }

    public void SkillLevelUP(int level)
    {
        SkillLevel = level;
    }

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
        
        if(this.IsValid())
        {
            Managers.Object.Despawn(this);
        }
    }
    #endregion
}
