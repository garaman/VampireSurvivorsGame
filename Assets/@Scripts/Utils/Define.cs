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
        Melee,
        Projectile,
        Etc,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public const int PLAYER_ID = 1;
    public const string EXP_JAM_PREFAB = "EXPJam.prefab";

}
