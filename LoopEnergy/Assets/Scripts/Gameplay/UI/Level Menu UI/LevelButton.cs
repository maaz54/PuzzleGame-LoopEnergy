using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EnergyLoop.Game.Gameplay.Utility;

namespace EnergyLoop.Game.Gameplay.UI
{
    /// <summary>
    /// Represents a UI button for selecting levels.
    /// </summary>
    public class LevelButton : MonoBehaviour
    {
        // The text component displaying the level number
        [SerializeField] TextMeshProUGUI textLevelNo;

        // The image indicating whether the level is locked
        [SerializeField] Image lockImage;

        // Data about the level's progress
        private LevelProgressData levelProgressData;

        [SerializeField] Button button;

        // Event invoked when the button is pressed
        public Action<LevelProgressData> OnButtonPressed;

        /// <summary>
        /// Initializes the level button with the specified level progress data.
        /// </summary>
        public void Initialize(LevelProgressData levelProgressData)
        {
            this.levelProgressData = levelProgressData;
            lockImage.gameObject.SetActive(levelProgressData.IsLocked);
            textLevelNo.text = levelProgressData.IsLocked ? "" : "Level: " + levelProgressData.LevelNo;
            button.onClick.AddListener(OnButtonPress);
        }

        /// <summary>
        /// Called when the button is pressed.
        /// </summary>
        private void OnButtonPress()
        {
            OnButtonPressed?.Invoke(levelProgressData);
        }
    }
}