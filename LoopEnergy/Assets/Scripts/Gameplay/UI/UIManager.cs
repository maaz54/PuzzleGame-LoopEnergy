using System;
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
        [SerializeField] Button nextLevelButton;

        /// Texts
        [SerializeField] TextMeshProUGUI currentScoreText;
        [SerializeField] TextMeshProUGUI finalScoreText;

        [SerializeField] LevelButton levelButtonPrefab;

        [SerializeField] GameObject levelButtonHolder;

        // Actions triggered by UI events
        public Action OnGameStartButton;
        public Action OnNextLevelButton;

        public Action<LevelProgressData> OnLevelButton;

        void Start()
        {
            gameStartButton.onClick.AddListener(OnGameStart);
            nextLevelButton.onClick.AddListener(OnNextLevel);

            EnablePanel(menuPanel);
        }

        // Event handler for game start button click

        private void OnGameStart()
        {
            OnGameStartButton?.Invoke();
        }

        // Start the game and display the gameplay panel
        public void PlayGame()
        {
            EnablePanel(GameplayPanel);
        }

        public void SetLevelDetails(LevelsProgression levelsProgression)
        {
            for (int i = 0; i < levelsProgression.LevelsProgressData.Count; i++)
            {
                LevelButton levelButton = Instantiate(levelButtonPrefab, levelButtonHolder.transform);
                levelButton.Initialize(levelsProgression.LevelsProgressData[i]);
                levelButton.OnButtonPressed += (level) => OnLevelButton?.Invoke(level);
            }
        }


        // Set the current score text
        public void SetScore(int score)
        {
            currentScoreText.text = "Score: " + score.ToString();
        }

        // Handle level completion event
        public void LevelComplete(int score)
        {
            finalScoreText.text = " : " + score.ToString();
            EnablePanel(LevelCompletePanel);
        }

        // Event handler for next level button click
        private void OnNextLevel()
        {
            OnNextLevelButton?.Invoke();
        }

        // Enable the specified panel and disable others
        private void EnablePanel(GameObject panel)
        {
            menuPanel.SetActive(false);
            GameplayPanel.SetActive(false);
            LevelCompletePanel.SetActive(false);
            LevelsPanel.SetActive(false);
            

            panel.SetActive(true);

            Debug.Log("s" + panel.name);
        }

    }
}