using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    float _spawnTime = 0.5f;
    int _maxMonsterCount = 50;
    Coroutine _coUpdateSpawningPool;

    public bool Stopped { get; set; } = false;

    void Start()
    {
        _coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
    }

    void Update()
    {
        
    }

    IEnumerator CoUpdateSpawningPool()
    {
        while (true) 
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    void TrySpawn()
    {
        if (Stopped) { return; }

        int monsterCount = Managers.Object.Monsters.Count;
        if(monsterCount >= _maxMonsterCount ) { return; }

        Vector3 randPos = Util.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position,5,10);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos,1+Random.Range(0,2));        
    }
}
