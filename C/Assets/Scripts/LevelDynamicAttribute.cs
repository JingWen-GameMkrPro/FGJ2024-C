using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDynamicAttribute : MonoBehaviour
{
    //�]�������׽վ��I
    public float WalkSpeed = 2f; // ���ʳt��
    public float JumpForce = 5f; //���D�O�D
    public float WalkTime = 2f; //���ʮɶ�
    public float WaitTime = 1f; //���ʶ��j���ݮɶ�
    public float DirectionChangeInterval = 1f; // ��V���ܶ��j�ɶ�

    public float BossSize = 1f; //�]���j�p
    public float MaxBossHP = 100f; //�]����q
    public float BossAttack = 10f; //�]�������O
    public float BossResistance = 1f; //�]����ܤO

    //�k�ͮĪG
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
