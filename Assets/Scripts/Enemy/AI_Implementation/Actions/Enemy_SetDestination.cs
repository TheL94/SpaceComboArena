﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_SetDestination")]
    public class Enemy_SetDestination : AI_Action
    {
        protected override bool Act(AI_Controller _controller)
        {
            return SetDestination((_controller as AI_Enemy).Enemy);
        }

        bool SetDestination(Enemy _enemy)
        {
            _enemy.Agent.SetDestination(_enemy.Target.Position);
            return true;
        }
    }
}

