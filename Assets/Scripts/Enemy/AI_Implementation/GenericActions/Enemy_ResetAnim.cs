﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFramework.AI;

namespace TeamF.AI
{
    [CreateAssetMenu(menuName = "AI/NewAction/Enemy_ResetAnim")]
    public class Enemy_ResetAnim : AI_Action
    {
        float timer = 0;
        float movementTime;
        protected override bool Act(AI_Controller _controller)
        {
            timer += Time.deltaTime;
            movementTime += Time.deltaTime;

            if (ResetAnimation((_controller as AI_Enemy).Enemy))
                return true;

            return false;
        }

        bool ResetAnimation(Enemy _enemy)
        {
            _enemy.transform.position = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y - movementTime * 1.5f, _enemy.transform.position.z);

            if (_enemy.Animator.GetInteger("State") == 0 && timer >= _enemy.Animator.GetCurrentAnimatorStateInfo(0).length)
            {
                return true;
            }
            else if (timer >= _enemy.Animator.GetCurrentAnimatorStateInfo(0).length)
            {
                _enemy.Animator.SetInteger("State", 0);
                timer = 0;
            }

            return false;
        }
    }
}
