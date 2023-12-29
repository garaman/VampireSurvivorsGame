using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{  

    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} , {count}/{totalCount}");
            if( count == totalCount ) // 로딩완료.
            {               
                StartLoaded();                
            }
        });
    }

    SpawningPool _spawningPool;
    void StartLoaded()
    {
        Managers.DataXml.Init();

        _spawningPool = gameObject.AddComponent<SpawningPool>();
        
        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);
               
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().target = player.gameObject;

        //Data Test
        Managers.DataXml.Init();

        foreach( var playerData in Managers.DataXml.PlayerDict.Values ) 
        {
            Debug.Log($"Level : {playerData.level} , Hp : {playerData.maxHp}");
        }
    }


    void Update()
    {
        
    }
}
