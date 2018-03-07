﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class EnemySpawner_TS : EnemyManager
    {
        public List<IDamageable> ManichiniDiDestinazione = new List<IDamageable>();

        public override void Init(IDamageable _enemyTarget, bool _isTestScene = false)
        {
            base.Init(_enemyTarget, _isTestScene);

            foreach (IDamageable dummy in GetComponentsInChildren<IDamageable>())
            {
                ManichiniDiDestinazione.Add(dummy);
            }

            SpawnEnemyForeachSpawn();
        }

        void SpawnEnemyForeachSpawn()
        {
            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                Enemy _newEnemy = SpawnEnemy(EnemyPrefab, SpawnPoints[i]);
                _newEnemy.Init(ManichiniDiDestinazione[i], FindEnemyDataByType((EnemyType)i + 2), Data.EnemyInitialState, "Enemy" + idCounter);
            }
        }
    }
}