using System;
using System.Collections.Generic;
using EnergyLoop.Game.Gameplay.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EnergyLoop.Game.Gameplay.Utility;

namespace EnergyLoop.Game.Gameplay.Manager.UI
{
    /// <summary>
    /// Manages the UI elements and interactions in the game.
    /// </summary>
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


        // Prefab for level buttons
        [SerializeField] LevelButton levelButtonPrefab;

        // Holder for level buttons
        [SerializeField] GameObject levelButtonHolder;

        // List of level buttons
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

        /// <summary>
        /// Handles tap to continue button click.
        /// </summary>
        private void TapToContinueButton()
        {
            EnablePanel(menuPanel);
            TapToContinue?.Invoke();
        }

        /// <summary>
        /// Starts the game and displays the gameplay panel.
        /// </summary>
        private void OnGameStart()
        {
            OnGameStartButton?.Invoke();
        }

        /// <summary>
        /// Starts the game and displays the gameplay panel.
        /// </summary>
        public void PlayGame(int levelNo)
        {
            levelNoText.text = "Level: " + levelNo.ToString();
            EnablePanel(GameplayPanel);
        }

        /// <summary>
        /// Sets the level details and initializes level buttons.
        /// </summary>
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

        /// <summary>
        /// Updates the level details for existing level buttons.
        /// </summary>
        public void UpdateLevelDetails(LevelsProgression levelsProgression)
        {
            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].Initialize(levelsProgression.LevelsProgressData[i]);
            }
        }


        /// <summary>
        /// Sets the current score text.
        /// </summary>
        public void SetScore(int turns)
        {
            currentScoreText.text = "Turns: " + turns.ToString();
        }

        /// <summary>
        /// Handles level completion event and displays the level complete panel.
        /// </summary>
        public void LevelComplete(int turns)
        {
            finalScoreText.text = "Turns: " + turns.ToString();
            EnablePanel(LevelCompletePanel);
        }

        /// <summary>
        /// Handles select level button click.
        /// </summary>
        private void OnSelectLevelButton()
        {
            EnablePanel(LevelsPanel);
        }

        /// <summary>
        /// Enables the specified panel and disables others.
        /// </summary>
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