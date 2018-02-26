﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class AmmoCrate : MonoBehaviour
    {
        public ElementalType Type { get; set; }

        [HideInInspector]
        public int Ammo;
        AmmoCratesController controller;

        public void Init(AmmoCratesController _controller, int _ammo)
        {
            controller = _controller;
            Ammo = _ammo;
            Type = (ElementalType)Random.Range(1, 5);
            MeshRenderer render = GetComponent<MeshRenderer>();
            switch (Type)
            {
                case ElementalType.None:
                    break;
                case ElementalType.Fire:
                    render.material.color = Color.red;
                    break;
                case ElementalType.Water:
                    render.material.color = Color.blue;
                    break;
                case ElementalType.Poison:
                    render.material.color = Color.green;
                    break;
                case ElementalType.Thunder:
                    render.material.color = Color.magenta;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Chiama la funzioen nel controller per rimuoverla dalla lista di crate, distruggerla, 
        /// avviare la coroutine per istanziare una nuova cassa al suo posto.
        /// </summary>
        public void DestroyAmmoCrate()
        {
            controller.DeleteAmmoCrateFromList(this);
        }
    }
}