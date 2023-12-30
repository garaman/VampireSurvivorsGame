using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    [SerializeField] Transform _indicator;
    [SerializeField] Transform _fireSoket;

    Vector2 _moveDir = Vector2.zero;

    float EnvCollectDist { get; set; } = 1.0f;
    

    public override bool Init()
    {
        if(base.Init() == false) { return false; }

        _speed = 5.0f;       

        StartProjectile();
        StartEgoSword();

        return true;
    }

    private void OnDestroy()
    {
        if(Managers.Game != null)
        {

        }
    }


    void Update()
    {        
        MovePlayer();
        CollectEnv();
    }

    void MovePlayer()
    {
        _moveDir = Managers.Game.MoveDir;

        Vector3 dir = _moveDir * _speed * Time.deltaTime;
        transform.position += dir;

        if(_moveDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0,0, Mathf.Atan2(-dir.x,dir.y)*180 / Mathf.PI);
        }

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;    
    }

    void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDist * EnvCollectDist;
        
        var FindEnv = GameObject.Find("@Grid").GetComponent<GridCell>().GatherObjects(transform.position, EnvCollectDist + 0.5f);

        foreach (var go in FindEnv)
        {
            Vector3 dir = go.transform.position - transform.position;          

            if (dir.sqrMagnitude <= sqrCollectDist)
            {
                Define.EnvType envType = go.GetComponent<EnvController>().EnvType;
                if (envType == Define.EnvType.Jam) 
                {                    
                    JamController jam = go.GetComponent<JamController>();
                    Managers.Game.Jam += 1;
                    Managers.Object.Despawn(jam);
                }
                else if (envType == Define.EnvType.Gold)
                {                    
                    GoldController glod = go.GetComponent<GoldController>();
                    Managers.Game.Gold += 10;
                    Managers.UI.GetSceneUI<UI_GameScene>().SetGoldCount(Managers.Game.Gold);
                    Managers.Object.Despawn(glod);
                }
            }
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController target = collision.gameObject.GetComponent<MonsterController>();
        if (target.IsVaild() == false) { return; }
        if (this.IsVaild() == false) { return; }
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
        //Debug.Log($"OnDamaged! {Hp}");

        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 1000);
    }

    // юс╫ц
    #region FireProjectile

    Coroutine _coFireProjectile;

    void StartProjectile()
    {
        if(_coFireProjectile != null)
        {
            StopCoroutine(_coFireProjectile);
        }
        _coFireProjectile = StartCoroutine(CoStartProjectile());
    }

    IEnumerator CoStartProjectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true) 
        {
            ProjectileController pc = Managers.Object.Spawn<ProjectileController>(_fireSoket.position,1);
            pc.SetInfo(1, this, (_fireSoket.position-_indicator.position).normalized);

            yield return wait;
        }
    }
    #endregion

    #region EgoSword
    EgoSwordController _egoSword;
    void StartEgoSword()
    {
        if (_egoSword.IsVaild()) { return; }

        _egoSword = Managers.Object.Spawn<EgoSwordController>(_indicator.position, Define.EGO_SWORD_ID);
        _egoSword.transform.SetParent(_indicator);

        _egoSword.ActiveateSkill();
    }
    #endregion
}
