﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class SpaceSceneController : MonoBehaviour
    {
        public delegate void OnEndAnimation();
        public static OnEndAnimation EndSpanceAnimation;

        int endedAnimations;

        private void OnEnable()
        {
            EndSpanceAnimation += UpdateAnimationsCount;
            GameManager.I.IsPlayingSequnce = true;
        }

        private void Start()
        {
            GameManager.I.UIMng.UI_GameplayCtrl.gameObject.SetActive(false);
        }

        void UpdateAnimationsCount()
        {
            endedAnimations++;
            GameManager.I.UIMng.UI_GameplayCtrl.gameObject.SetActive(true);

            if (endedAnimations == 2)
                GameManager.I.CurrentState = FlowState.ManageMap;
        }

        private void OnDisable()
        {
            EndSpanceAnimation -= UpdateAnimationsCount;
            GameManager.I.IsPlayingSequnce = false;
        }
    }
}