using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public PlayerController Player { get { return Managers.Object?.Player; } }

    #region 재화
    public int Gold { get; set; }

    int _jam = 0;
    public event Action<int> OnJamCountChanged;
    public int Jam 
    {
        get { return _jam; }
        set
        {
            _jam = value;
            OnJamCountChanged?.Invoke(value);
        }
    }
    #endregion

    #region 이동
    Vector2 _moveDir = Vector2.zero;
    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }
    #endregion

    #region 전투
    int _killCount;
    public event Action<int> OnKillCountChanged;

    public int KillCount
    {
        get { return _killCount; }
        set 
        { 
            _killCount = value;
            OnKillCountChanged?.Invoke(value);
        }
    }
    #endregion

    #region 레벨
    int _level;
    public event Action<int> OnLevelChanged;

    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            OnLevelChanged?.Invoke(value);
        }
    }
    #endregion


    public void SaveData()
    {
        PlayerPrefs.SetInt("Gold", Gold);

    }
}
