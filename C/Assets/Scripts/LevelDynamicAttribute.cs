using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDynamicAttribute : MonoBehaviour
{
    public enum LevelType
    {
        VeryEasy,
        Easy,
        Normal,
        Hard,
        Hell,
    }
    public LevelType levelType;
    public void SetDataByLevelType()
    {
        levelType = GameController.Instance.levelType;

        switch(levelType)
        {
            case LevelType.VeryEasy:
                WalkSpeed = 1f;
                JumpForce = 2f;
                WalkTime = 2f;
                WaitTime = 1f;
                DirectionChangeInterval = 1f;

                BossSize = 0.5f;
                MaxBossHP = 50f;
                BossAttack = 5f;
                BossResistance = 0.5f;
                break;
            case LevelType.Easy:
                WalkSpeed = 1.5f;
                JumpForce = 3f;
                WalkTime = 2f;
                WaitTime = 1f;
                DirectionChangeInterval = 1f;

                BossSize = 0.7f;
                MaxBossHP = 70f;
                BossAttack = 7f;
                BossResistance = 0.7f;
                break;
            case LevelType.Normal:
                WalkSpeed = 2f;
                JumpForce = 4f;
                WalkTime = 2f;
                WaitTime = 1f;
                DirectionChangeInterval = 1f;

                BossSize = 1f;
                MaxBossHP = 100f;
                BossAttack = 10f;
                BossResistance = 1f;
                break;
            case LevelType.Hard:
                WalkSpeed = 2.5f;
                JumpForce = 5f;
                WalkTime = 2f;
                WaitTime = 1f;
                DirectionChangeInterval = 1f;

                BossSize = 1.2f;
                MaxBossHP = 120f;
                BossAttack = 12f;
                BossResistance = 1.2f;
                break;
            case LevelType.Hell:
                WalkSpeed = 3f;
                JumpForce = 6f;
                WalkTime = 2f;
                WaitTime = 1f;
                DirectionChangeInterval = 1f;

                BossSize = 1.5f;
                MaxBossHP = 150f;
                BossAttack = 15f;
                BossResistance = 1.5f;
                break;
        }
    }

    //魔王難易度調整點
    public float WalkSpeed = 2f; // 移動速度
    public float JumpForce = 5f; //跳躍力道
    public float WalkTime = 2f; //移動時間
    public float WaitTime = 1f; //移動間隔等待時間
    public float DirectionChangeInterval = 1f; // 方向改變間隔時間

    public float BossSize = 1f; //魔王大小
    public float MaxBossHP = 100f; //魔王血量
    public float BossAttack = 10f; //魔王攻擊力
    public float BossResistance = 1f; //魔王抵抗力

    //女友效果
    public enum GirlfriendEffect
    {
        None,
        Poison,
        Cool,
        Complain,
        HotHeart,
        LovePunch,
        LoveKiss,
    }
}
