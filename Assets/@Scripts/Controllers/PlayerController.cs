using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    public PlayerController Player { get { return Managers.Object?.Player; }  }

    [SerializeField] Transform _indicator;
    [SerializeField] Transform _fireSoket;

    #region 재화
    public int Gold {  get; set; }
    public int Jam { get; set; }
    #endregion

    #region 이동
    Vector2 _moveDir = Vector2.zero;
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }
    #endregion
    
    float EnvCollectDist { get; set; } = 1.0f;
    

    public override bool Init()
    {
        if(base.Init() == false) { return false; }

        _speed = 5.0f;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        StartProjectile();

        return true;
    }

    private void OnDestroy()
    {
        if(Managers.Game != null)
        {
            Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        }
    }
    void HandleOnMoveDirChanged(Vector2 moveDir)
    {
        _moveDir = moveDir;
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

        List<JamController> Jams = Managers.Object.Jams.ToList();
        foreach(JamController jam in Jams)
        {
            Vector3 dir = jam.transform.position - transform.position;
            if(dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Game.Player.Jam += 1;
                Managers.Object.Despawn(jam);
            }
        }

        var FindJams = GameObject.Find("@Grid").GetComponent<GridCell>().GatherObjects(transform.position, EnvCollectDist + 0.5f);

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

    // 임시
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
}
