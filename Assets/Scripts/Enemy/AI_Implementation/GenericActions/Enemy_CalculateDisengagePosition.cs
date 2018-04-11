﻿using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_CalculateDisengagePosition")]
    public class Enemy_CalculateDisengagePosition : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            CalculateRangedAttackPosition((_controller as AI_Enemy).Enemy);
            return true;
        }

        void CalculateRangedAttackPosition(Enemy _enemy)
        {
            if (!_enemy.AI_Enemy.IsDisengaging) 
            {
                _enemy.AI_Enemy.IsDisengaging = true;
                Vector3 disengagePosition = (_enemy.Target.Position - _enemy.Position).normalized;
                disengagePosition *= _enemy.Data.RangedDamageRange - Random.Range(0.05f, _enemy.Data.RangeOffset);
                _enemy.Agent.destination = _enemy.Target.Position + disengagePosition;
            }
        }
    }
}
