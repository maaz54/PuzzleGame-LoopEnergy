using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnergyLoop.Game.Gameplay.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textLevelNo;
        [SerializeField] Image lockImage;
        private LevelProgressData levelProgressData;
        [SerializeField] Button button;

        public Action<LevelProgressData> OnButtonPressed;

        public void Initialize(LevelProgressData levelProgressData)
        {
            this.levelProgressData = levelProgressData;
            lockImage.gameObject.SetActive(levelProgressData.IsLocked);
            textLevelNo.text = levelProgressData.IsLocked ? "" : "Level: " + levelProgressData.LevelNo;
            button.onClick.AddListener(OnButtonPress);
        }

        private void OnButtonPress()
        {
            OnButtonPressed?.Invoke(levelProgressData);
        }
    }
}