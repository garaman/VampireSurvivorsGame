using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    protected float _speed = 3.0f;

    public int Hp { get; protected set; } = 100;
    public int MaxHp { get; protected set; } = 100;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public virtual void OnDamaged(BaseController attacker, int  damage)
    {        
        Hp -= damage;
        if(Hp < 0)
        {
            Hp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead() 
    { 
    
    }
}
