using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : SkillController
{
    CreatureController _owner;
    Vector3 _moveDir;
    float _speed = 10.0f;
    float _lifeTime = 10.0f;

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
    }

    public override void UpdateController()
    {
        base.UpdateController();

        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if(mc.IsVaild() == false) { return; }
        if(this.IsVaild() == false) { return; }

        mc.OnDamaged(_owner, SkillData.damage);

        StopDestroy();

        Managers.Object.Despawn(this);
    }
}
