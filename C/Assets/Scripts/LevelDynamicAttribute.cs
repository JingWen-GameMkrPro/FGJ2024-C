using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDynamicAttribute : MonoBehaviour
{
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
