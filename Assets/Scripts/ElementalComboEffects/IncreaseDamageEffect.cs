﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class IncreaseDamageEffect : IElementalEffectBehaviour
    {
        IEffectable target;
        ElementalEffectData elementalData;
        float startMultiplyer;

        public void DoInit(IEffectable _target, ElementalEffectData _data)
        {
            target = _target;
            elementalData = _data;
            startMultiplyer = target.DamagePercentage;
            target.DamagePercentage = _data.EffectValue;
            (target as MonoBehaviour).GetComponentInChildren<ParticlesController>().ActivateParticles(ParticlesController.ParticleType.IncreaseDamage);
        }

        public void DoStopEffect()
        {
            if(target != null)
            {
                target.DamagePercentage = startMultiplyer;
                (target as MonoBehaviour).GetComponentInChildren<ParticlesController>().StopAllParticles();
            }
        }

        public bool DoUpdate()
        {
            elementalData.TimeOfEffect -= Time.deltaTime;
            if (elementalData.TimeOfEffect <= 0)
            {
                return true;
            }
            return false;
        }
    }
}