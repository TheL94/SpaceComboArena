﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamF
{
    public class LevelManager : MonoBehaviour
    {
        public LevelEndingStaus EndingStaus { get; set; }

        float PointsToWin;
        float roundPoints = 0;

        public int Level
        {
            get
            {
                int levelIndex = 0;
                if (MapIndex == 1)
                    levelIndex = 0;
                else if (MapIndex > 1 && MapIndex < 5)
                    levelIndex = MapIndex - 1;
                else if (MapIndex == 5)
                    levelIndex = 0;
                else if (MapIndex > 5 && MapIndex < 9)
                    levelIndex = MapIndex - 2;
                else if (MapIndex == 9)
                    levelIndex = 0;
                else if (MapIndex > 9 && MapIndex < 13)
                    levelIndex = MapIndex - 3;
                Debug.Log(levelIndex);
                return levelIndex;
            }
        }

        public void Init(float _pointsToWin)
        {
            PointsToWin = _pointsToWin;
            Events_LevelController.OnKillPointChanged += UpdateRoundPoints;
        }

        #region Scene Management
        AsyncOperation async;

        public int TotalMaps { get { return SceneManager.sceneCountInBuildSettings; } }
        int _mapIndex  = 0;
        public int MapIndex { get { return _mapIndex; } set { OnLevelChange(value); } }

        void OnLevelChange(int _newLevel)
        {
            GameManager.I.UIMng.LoadingActions();
            UnloadLevel(MapIndex, _newLevel);
        }

        void UnloadLevel(int _currentLevel, int _newLevel)
        {
            if (_currentLevel > 0)
                StartCoroutine(DectivateScene(_currentLevel, _newLevel));
            else
                LoadNewLevel(_newLevel);
        }

        IEnumerator DectivateScene(int _currentLevel, int _newLevel)
        {
            while (!GameManager.I.EnemyMng.IsFreeToGo)
                yield return null;

            async = SceneManager.UnloadSceneAsync(_currentLevel);
            if(async != null)
            {
                async.completed += (async) =>
                {
                    LoadNewLevel(_newLevel);
                };
            }
        }

        void LoadNewLevel(int _newLevel)
        {
            if (_newLevel != _mapIndex && _newLevel != 0 && _newLevel < TotalMaps)
            {
                async = SceneManager.LoadSceneAsync(_newLevel, LoadSceneMode.Additive);
                async.completed += (async) =>
                {
                    StartCoroutine(ActivateScene(_newLevel));
                };
            }
            else if (_newLevel == 0 || _newLevel >= TotalMaps)
            {
                _mapIndex = 0;
                GameManager.I.CurrentState = FlowState.MainMenu;
            }
        }

        IEnumerator ActivateScene(int _newLevel)
        {
            while (!GameManager.I.PoolMng.IsFreeToGo)
                yield return null;

            _mapIndex = _newLevel;
            async.allowSceneActivation = true;
            if (GameManager.I.CurrentState == FlowState.ManageMap)
                GameManager.I.CurrentState = FlowState.InitGameplayElements;
            else if (GameManager.I.CurrentState == FlowState.InitTestScene)
                GameManager.I.CurrentState = FlowState.TestGameplay;
        }
        #endregion

        #region API

        public void CheckGameStatus()
        {
            if (GameManager.I.CurrentState != FlowState.Gameplay)
                return;

            if (roundPoints >= PointsToWin)
            {
                EndingStaus = LevelEndingStaus.Won;
                GameManager.I.CurrentState = FlowState.PreEndRound;
                Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;

                return;
            }

            if(GameManager.I.Player.Character.Life <= 0)
            {
                EndingStaus = LevelEndingStaus.Lost;
                GameManager.I.CurrentState = FlowState.PreEndRound;
                Events_LevelController.OnKillPointChanged -= UpdateRoundPoints;
                return;
            }
        }

        public void ReInit()
        {
            roundPoints = 0;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);
            EndingStaus = LevelEndingStaus.NotEnded;
        }

        [HideInInspector]
        public List<ElementalComboBase> Combos = new List<ElementalComboBase>();
        public void ClearCombos() 
        {
            for (int i = 0; i < Combos.Count; i++)
                Combos[i].EndEffect();
        }
        #endregion

        void UpdateRoundPoints(float _killedEnemyValue)
        {
            roundPoints += _killedEnemyValue;
            if (roundPoints >= PointsToWin)
                roundPoints = PointsToWin;
            Events_UIController.KillPointsChanged(roundPoints, PointsToWin);

            CheckGameStatus();
        }
    }

    public enum LevelEndingStaus { NotEnded = 0, Won, Lost, Interrupted }
}

