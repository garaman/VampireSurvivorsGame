using DataXml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : SkillBase
{
    CreatureController _owner;
    Vector3 _moveDir;
    float _speed = 10.0f;
    float _lifeTime = 10.0f;
    int _level = 0;

    public ProjectileController() : base(Define.SkillType.None)
    {

    }

    public override bool Init()
    {
        base.Init();

        StartDestroy(_lifeTime);

        return true;
    }

    public void SetInfo(int templateID, CreatureController owner,  Vector3 moveDir)
    {
        if(Managers.DataXml.SkillDict.TryGetValue(templateID, out DataXml.SkillData data) == false) 
        {
            Debug.LogError("ProjecteController SetInfo Failed");
            return;
        }

        _owner = owner;
        _moveDir = moveDir;
        SkillData = data;
               
        while(true) 
        {
            if(_level == SkillData.level) { break; }
            switch (templateID)
            {
                case (int)Define.FIRE_BALL_ID:
                    SkillData.speed = _speed + (SkillData.level * 2);
                    break;
                case (int)Define.WIND_CUTTER_ID:
                    SkillData.damage += (SkillData.level * 20);
                    break;
            }
            _level++;
            if (_level >= SkillData.level) { _level = SkillData.level; break; }
        }
        
        _speed = SkillData.speed;        

    }

    public override void UpdateController()
    {
        base.UpdateController();

        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if(mc.IsValid() == false) { return; }
        if(this.IsValid() == false) { return; }
                
        mc.OnDamaged(_owner, SkillData.damage);

        StopDestroy();

        if(SkillData.templateID == Define.WIND_CUTTER_ID) { return; }
        Managers.Object.Despawn(this);
    }
}
