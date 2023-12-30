using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    float _spawnTime = 0.1f;
    int _maxMonsterCount = 100;
    Coroutine _coUpdateSpawningPool;

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
        int monsterCount = Managers.Object.Monsters.Count;
        if(monsterCount >= _maxMonsterCount ) { return; }

        Vector3 randPos = Util.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position,5,10);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos,Random.Range(0,2));        
    }
}
