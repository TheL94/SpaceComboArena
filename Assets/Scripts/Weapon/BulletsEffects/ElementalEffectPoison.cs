﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class ElementalEffectPoison : IElementalEffectBehaviour
    {
        Enemy enemy;
        ElementalEffectData elementalData;
        float timer;

        public void DoInit(Enemy _enemy, ElementalEffectData _data)
        {
            enemy = _enemy;
            elementalData = _data;
            timer -= elementalData.TimeFraction;
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= timer)
            {
                if (elementalData.EffectValue > 0)
                    enemy.TakeDamage(elementalData.EffectValue, ElementalType.Poison);

                timer -= elementalData.TimeFraction;
            }
            if (elementalData.TimeOfEffect <= 0)
                return true;
            else
                return false;
        }


        public void DoStopEffect() { }
    }
}