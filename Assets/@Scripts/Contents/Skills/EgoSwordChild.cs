using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgoSwordChild : MonoBehaviour
{
    BaseController _owner;
    int _damage;

    public void SetInfo(BaseController owner, int damage)
    {
        _owner = owner;
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.GetComponent<MonsterController>();
        if(mc.IsVaild() == false) { return; }

        mc.OnDamaged(_owner, _damage);
    }
}
