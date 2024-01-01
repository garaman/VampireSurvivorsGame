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
    Define.StageType _stageType;
    public Define.StageType StageType
    {
        get { return _stageType; }
        set
        {
            _stageType = value;
            if(_spawningPool != null ) 
            { 
                switch(value)
                {
                    case Define.StageType.Normal:
                        _spawningPool.Stopped = false;
                        break;
                    case Define.StageType.Boss:
                        _spawningPool.Stopped = true;
                        break;
                }
            }
        }
    }
    void StartLoaded()
    {
        Managers.DataXml.Init();

        Managers.UI.ShowSceneUI<UI_GameScene>();

        _spawningPool = gameObject.AddComponent<SpawningPool>();
        
        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);
               
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().target = player.gameObject;

        //Data Test
        Managers.DataXml.Init();

        Managers.Game.OnJamCountChanged -= HandleOnJamCountChanged;
        Managers.Game.OnJamCountChanged += HandleOnJamCountChanged;
        Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
        Managers.Game.OnKillCountChanged += HandleOnKillCountChanged;
        Managers.Game.OnLevelChanged -= HandleOnLevelChanged;
        Managers.Game.OnLevelChanged += HandleOnLevelChanged;

    }

    int _collectedJamCount = 0;
    int _remainingTotalJameCount = 1;

    public void HandleOnJamCountChanged(int jamCount)
    {
        _collectedJamCount++;
        
        if(_collectedJamCount == _remainingTotalJameCount)
        {
            Managers.UI.ShowPopup<UI_SkillSelectPopup>();
            _collectedJamCount = 0;
            _remainingTotalJameCount *= 2;
            Managers.Game.Level++;
        }

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)_collectedJamCount / _remainingTotalJameCount);
    }

    public void HandleOnKillCountChanged(int killCount)
    {
        Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);

        if(killCount == 20)
        {
            //BOSS
            StageType = Define.StageType.Boss;

            Managers.Object.DespawnAllMonsters();

            Vector2 spawnPos = Util.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 5, 10);

            Managers.Object.Spawn<MonsterController>(spawnPos, Define.BOSS_ID);
        }
    }

    public void HandleOnLevelChanged(int Level)
    {        
        Managers.UI.GetSceneUI<UI_GameScene>().SetLevel(Level);
        Managers.UI.GetSceneUI<UI_SkillSelectPopup>().SetLevel(Level);
    }

    private void OnDestroy()
    {
        if(Managers.Game != null)
        {
            Managers.Game.OnJamCountChanged -= HandleOnJamCountChanged;
            Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
            Managers.Game.OnLevelChanged -= HandleOnLevelChanged;

        }
    }
}
