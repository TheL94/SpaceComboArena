﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamF
{
    public class UIManager : MonoBehaviour
    {
        public UI_GameplayController UI_GameplayCtrl;
        public UI_MainMenuController UI_MainMenuCtrl;
        public UI_GameOverController UI_GameOverCtrl;
        public UI_ValuesPanelController UI_ValuesPanelCtrl;
        public UI_PauseController UI_PauseController;

        [HideInInspector]
        public MenuBase CurrentMenu;

        public void MainMenuActions()
        {
            UI_ValuesPanelCtrl.gameObject.SetActive(false);
            UI_MainMenuCtrl.gameObject.SetActive(true);
            UI_GameOverCtrl.gameObject.SetActive(false);
            UI_PauseController.gameObject.SetActive(false);
            UI_MainMenuCtrl.Init();
        }

        ///TODO: qualcuno deve richiamare questa funzione passando i valori già settati dei dati, in modo che vengano scritti nei campi presenti
        ///nel pannello dei valori
        public void EnableValuesPanel(CharacterData _characterData, EnemyGenericData _enemyData)
        {
            UI_MainMenuCtrl.gameObject.SetActive(false);
            UI_GameOverCtrl.gameObject.SetActive(false);
            UI_ValuesPanelCtrl.gameObject.SetActive(true);
            UI_PauseController.gameObject.SetActive(false);
            UI_ValuesPanelCtrl.Init(_characterData, _enemyData);
        }

        public void GameplayActions()
        {
            UI_ValuesPanelCtrl.gameObject.SetActive(false);
            UI_MainMenuCtrl.gameObject.SetActive(false);
            UI_GameOverCtrl.gameObject.SetActive(false);
            UI_PauseController.gameObject.SetActive(false);
        }

        public void PauseActions()
        {
            UI_ValuesPanelCtrl.gameObject.SetActive(false);
            UI_MainMenuCtrl.gameObject.SetActive(false);
            UI_GameOverCtrl.gameObject.SetActive(false);
            UI_PauseController.gameObject.SetActive(true);
            UI_PauseController.Init();
        }

        public void GameOverActions(LevelEndingStaus _levelStatus)
        {
            UI_ValuesPanelCtrl.gameObject.SetActive(false);
            UI_MainMenuCtrl.gameObject.SetActive(false);
            UI_GameOverCtrl.gameObject.SetActive(true);
            UI_GameOverCtrl.Init(_levelStatus);
        }
    }
}