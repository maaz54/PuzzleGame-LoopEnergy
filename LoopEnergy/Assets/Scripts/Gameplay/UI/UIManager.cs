using System;
using System.Collections.Generic;
using EnergyLoop.Game.Gameplay.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnergyLoop.Game.Gameplay.Manager.UI
{
    public class UIManager : MonoBehaviour
    {
        /// UI Panels
        [SerializeField] GameObject menuPanel;
        [SerializeField] GameObject GameplayPanel;
        [SerializeField] GameObject LevelCompletePanel;
        [SerializeField] GameObject LevelsPanel;

        /// UI Buttons
        [SerializeField] Button gameStartButton;
        [SerializeField] Button tapToContinueButton;
        [SerializeField] Button selectLevelButton;
        [SerializeField] Button backButtonLevelScreen;

        /// Texts
        [SerializeField] TextMeshProUGUI currentScoreText;
        [SerializeField] TextMeshProUGUI finalScoreText;
        [SerializeField] TextMeshProUGUI levelNoText;


        [SerializeField] LevelButton levelButtonPrefab;
        [SerializeField] GameObject levelButtonHolder;

        [SerializeField] List<LevelButton> levelButtons;

        // Actions triggered by UI events
        public Action OnGameStartButton;
        public Action TapToContinue;
        public Action<LevelProgressData> OnLevelButton;

        void Start()
        {
            gameStartButton.onClick.AddListener(OnGameStart);
            tapToContinueButton.onClick.AddListener(TapToContinueButton);
            selectLevelButton.onClick.AddListener(OnSelectLevelButton);
            backButtonLevelScreen.onClick.AddListener(() => EnablePanel(menuPanel));


            EnablePanel(menuPanel);
        }

        private void TapToContinueButton()
        {
            EnablePanel(menuPanel);
            TapToContinue?.Invoke();
        }

        // Event handler for game start button click

        private void OnGameStart()
        {
            OnGameStartButton?.Invoke();
        }

        // Start the game and display the gameplay panel
        public void PlayGame(int levelNo)
        {
            levelNoText.text = "Level: " + levelNo.ToString();
            EnablePanel(GameplayPanel);
        }

        public void SetLevelDetails(LevelsProgression levelsProgression)
        {
            for (int i = 0; i < levelsProgression.LevelsProgressData.Count; i++)
            {
                LevelButton levelButton = Instantiate(levelButtonPrefab, levelButtonHolder.transform);
                levelButton.Initialize(levelsProgression.LevelsProgressData[i]);
                levelButton.OnButtonPressed += (level) => OnLevelButton?.Invoke(level);
                levelButtons.Add(levelButton);
            }
        }

        public void UpdateLevelDetails(LevelsProgression levelsProgression)
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].Initialize(levelsProgression.LevelsProgressData[i]);
            }
        }


        // Set the current score text
        public void SetScore(int turns)
        {
            currentScoreText.text = "Turns: " + turns.ToString();
        }

        // Handle level completion event
        public void LevelComplete(int turns)
        {
            finalScoreText.text = "Turns: " + turns.ToString();
            EnablePanel(LevelCompletePanel);
        }

        private void OnSelectLevelButton()
        {
            EnablePanel(LevelsPanel);
        }

        // Enable the specified panel and disable others
        private void EnablePanel(GameObject panel)
        {
            menuPanel.SetActive(false);
            GameplayPanel.SetActive(false);
            LevelCompletePanel.SetActive(false);
            LevelsPanel.SetActive(false);

            panel.SetActive(true);
        }

    }
}