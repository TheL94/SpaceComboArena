﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamF
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager I;

        public FlowState CurrentState { get { return flowMng.CurrentState; } }

        FlowManager flowMng;
        public LevelManager LevelMng;
        public EnemyController EnemyCtrl;

        public AmmoCratesController AmmoController;

        [HideInInspector]
        public UIManager UIMng;
        public GameObject UIManagerPrefab;
        [HideInInspector]
        public Player Player;

        void Awake()
        {
            //Singleton paradigm
            if (I == null)
                I = this;
            else
                DestroyImmediate(gameObject);
        }

        void Start()
        {
            flowMng = new FlowManager();
            ChangeFlowState(FlowState.Loading);
            // NELLO START NON CI INFILARE NIENTE ! USA L'AZIONE DI LOADING
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseApplicationActions();
        }

        void ClearScene()
        {
            EnemyCtrl.EndGameplayActions();
        }

        #region API
        #region Game Flow
        /// <summary>
        /// Funzione che innesca il cambio di stato
        /// </summary>
        public void ChangeFlowState(FlowState _stateToSet)
        {
            flowMng.ChageState(_stateToSet);
        }

        public void LoadingActions()
        {
            UIMng = Instantiate(UIManagerPrefab, transform).GetComponentInChildren<UIManager>();

            Player = GetComponent<Player>();
            if (Player != null)
                Player.Init();

            ChangeFlowState(FlowState.Menu);
        }

        public void MenuActions()
        {
            UIMng.MainMenuActions();
        }

        public void EnterGameplayActions()
        {
            LevelMng = new LevelManager(50f);

            Player player = FindObjectOfType<Player>();
            if (player != null)
                player.CharacterInit();

            UIMng.GameplayActions();
            AmmoController.Init();
            EnemyCtrl.Init();
            ChangeFlowState(FlowState.Gameplay);
        }

        public void PauseActions()
        {

        }

        public void GameWonActions()
        {
            UIMng.GameOverActions(true);
            ChangeFlowState(FlowState.ExitGameplay);
        }

        public void GameLostActions()
        {
            UIMng.GameOverActions(false);
            ChangeFlowState(FlowState.ExitGameplay);
        }

        public void ExitGameplayActions()
        {
            ClearScene();
        }

        public void CloseApplicationActions()
        {
            Application.Quit();
        }
        #endregion

        /// <summary>
        /// Attiva il pannello dei valori e gli passa i dati per settare i campi all'inizio
        /// </summary>
        public void EnterValuesMenu()
        {
            UIMng.EnableValuesPanel(Player.GetCharacterData(), EnemyCtrl.SpawnerData.EnemiesData[0]);               // Farsi restituire i dati dal data manager
        }

        //TODO: Operazioni da svolgere dopo aver settato i valori del pannello dei valori
        public void EnterTestScene()
        {
            if (Player != null)
                Player.CharacterInit(true);

            UIMng.GameplayActions();
            EnemyCtrl.Init(true);
            ChangeFlowState(FlowState.Gameplay);
        }

        #endregion
    }
}