﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public EnemyType EnemyType;
        public ElementalType ElementalType
        {
            get
            {
                switch (EnemyType)
                {
                    case EnemyType.Melee:
                        return ElementalType.None;
                    case EnemyType.Ranged:
                        return ElementalType.None;
                    case EnemyType.Fire:
                        return ElementalType.Fire;
                    case EnemyType.Water:
                        return ElementalType.Water;
                    case EnemyType.Poison:
                        return ElementalType.Poison;
                    case EnemyType.Thunder:
                        return ElementalType.Thunder;
                }
                return ElementalType.None;
            }
        }
        public GameObject ContainerPrefab;
        public GameObject GraphicPrefab;

        public float Life;
        public float Speed;

        public float MeleeDamage;
        public float MeleeDamageRange;
        public float MeleeDamageRate;

        public float RangedDamage;
        public float RangedDamageRange;
        public float RangedDamageRate;

        public float EnemyValue;
    }
}