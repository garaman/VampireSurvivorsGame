using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : CreatureController
{

    void Start()
    {
        
    }


    void Update()
    {
        PlayerController pc = Managers.Object.Player;
        if(pc == null) { return; }

        Vector3 dir = pc.transform.position - transform.position;
        Vector3 newPos = transform.position + dir.normalized*Time.deltaTime*_speed;
        GetComponent<Rigidbody2D>().MovePosition(newPos);

        GetComponent<SpriteRenderer>().flipX = (dir.x > 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target.IsVaild() == false) { return; }
        if (this.IsVaild() == false) { return; }

        if (_coDotDamage != null) { StopCoroutine(_coDotDamage); }
        _coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target.IsVaild() == false) { return; }
        if (this.IsVaild() == false) { return; }

        if (_coDotDamage != null) { StopCoroutine(_coDotDamage); }
        _coDotDamage = null;
    }

    Coroutine _coDotDamage;
    public IEnumerator CoStartDotDamage(PlayerController target)
    {
        while(true) 
        {
            target.OnDamaged(this, 2);
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();

        Managers.Game.KillCount++;

        if(_coDotDamage != null) { StopCoroutine(_coDotDamage); }
        _coDotDamage = null;

        // Á×À» ¶§ º¸¼®, °ñµå ½ºÆù
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
