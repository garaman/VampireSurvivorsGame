using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

    public enum Scene
    {
        Unkown,
        DevScene,
        GameScene,
    }

    public enum ObjectType
    {
        Player,
        Monster,
        Projectile,
        Env,
    }

    public enum SkillType
    {
        None,
        Sequence,
        Repeat
    }

    public enum EnvType
    { 
        None,
        Jam,
        Gold
    }

    public enum StageType
    {
        Normal,
        Boss,
    }

    public enum CreatureState
    { 
        Idle,
        Moving,
        Skill,
        Dead,
    }

    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        Drag,
        BeginDrag,
        EndDrag,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public const int GOBLIN_ID = 1;
    public const int SNAKE_ID = 2;
    public const int BOSS_ID = 3;

    public const int PLAYER_ID = 1;
    
    public const string EXP_JAM_PREFAB = "EXPJam.prefab";
    public const string GOLD_PREFAB = "Gold.prefab";

    public const int FIRE_BALL_ID = 1;
    public const int EGO_SWORD_ID = 2;
    public const int WIND_CUTTER_ID = 3;

}
