using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : BaseController
{
    protected float _speed = 3.0f;

    public float Hp { get; protected set; } = 100;
    public float MaxHp { get; protected set; } = 100;

    public SkillBook Skills { get; protected set; }


    protected SpriteRenderer sprite;
    protected Material _originMaterial;
    protected Material _flashMaterial;

    public override bool Init()
    {
        base.Init();

        Skills = gameObject.GetOrAddComponent<SkillBook>();

        _flashMaterial = Managers.Resource.Load<Material>("Flash.mat");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        _originMaterial = sprite.material;
                
        return true;
    }

    public virtual void OnDamaged(BaseController attacker, float  damage)
    {

        StartCoroutine(Flash());
        Hp -= damage;            
        

        CreateDamageText(damage);
        
            
        if (Hp <= 0)
        {
            Hp = 0;
            sprite.material = _originMaterial;
            OnDead();
        }
    }

    protected virtual void OnDead() 
    { 
    
    }


    public IEnumerator Flash()
    {
        sprite.material = _flashMaterial;
        yield return new WaitForSeconds(0.2f);
        sprite.material = _originMaterial;
    }

    public void CreateDamageText(float damage)
    {
        if (ObjType == Define.ObjectType.Player) { return; }
        GameObject damageText = Managers.Resource.Instantiate("DamageText.prefab", pooling: true);        
        damageText.transform.position = gameObject.transform.position;
        
        damageText.GetComponent<TMP_Text>().text = $"{damage}";        
    }

}
