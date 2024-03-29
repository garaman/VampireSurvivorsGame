using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : CreatureController
{
    #region State Pattern

    Define.CreatureState _creatureState = Define.CreatureState.Moving;
    public virtual Define.CreatureState CreatureState
    {
        get { return _creatureState; }
        set
        {
            _creatureState = value;
            UpdateAnimation();
        }
    }

    protected Animator _animator;
    public virtual void UpdateAnimation()
    {

    }

    public override void UpdateController()
    {
        base.UpdateController();

        switch (CreatureState)
        {
            case Define.CreatureState.Idle:
                UpdateIdle();
                break;
            case Define.CreatureState.Skill:
                UpdateSkill();
                break;
            case Define.CreatureState.Moving:
                UpdateMoving();
                break;
            case Define.CreatureState.Dead:
                UpdateDead();
                break;
        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateDead() { }

    #endregion

    public override bool Init()
    {
        base.Init();        

        _animator = GetComponent<Animator>();
        ObjType = Define.ObjectType.Monster;
        CreatureState = Define.CreatureState.Moving;

        return true;
    }

    void FixedUpdate()
    {
        if (CreatureState != Define.CreatureState.Moving)
            return;
        PlayerController pc = Managers.Object.Player;
        if (pc == null)
            return;

        Vector3 dir = pc.transform.position - transform.position;
        Vector3 newPos = transform.position + dir.normalized * Time.deltaTime * _speed;
        GetComponent<Rigidbody2D>().MovePosition(newPos);

        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target.IsValid() == false) { return; }
        if (this.IsValid() == false) { return; }

        if (_coDotDamage != null) { StopCoroutine(_coDotDamage); }
        _coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target.IsValid() == false) { return; }
        if (this.IsValid() == false) { return; }

        if (_coDotDamage != null) { StopCoroutine(_coDotDamage); }
        _coDotDamage = null;
    }

    Coroutine _coDotDamage;
    public IEnumerator CoStartDotDamage(PlayerController target)
    {
        while(true) 
        {
            target.OnDamaged(this, 2-(2*Managers.Shop.Defence));
            yield return new WaitForSeconds(1.0f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();

        Managers.Game.KillCount++;

        if(_coDotDamage != null) { StopCoroutine(_coDotDamage); }
        _coDotDamage = null;

        // ���� �� ����, ��� ����
        int dropItem = Random.Range(0, 10);

        if (dropItem <= 8)
        {
            Managers.Object.Spawn<JamController>(transform.position);
        }
        else if (dropItem <= 9) 
        {
            Managers.Object.Spawn<GoldController>(transform.position);
        }

        Managers.Object.Despawn(this);        
    }

    
}
