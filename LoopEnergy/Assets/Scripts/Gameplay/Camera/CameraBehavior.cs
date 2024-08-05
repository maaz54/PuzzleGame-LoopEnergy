using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Manager
{
    /// <summary>
    /// Contains logic for adjust camera size according to grid 
    /// contains animations like shake effect
    /// </summary>
    public class CameraBehavior : MonoBehaviour
    {
        [SerializeField] Camera Camera;

        [SerializeField] float shakeDuration = 0.5f;
        [SerializeField] float shakeMagnitude = 0.1f;
        private Vector3 originalPos;


        public void AdjustCameraSize(int gridSizeX, int gridSizeY)
        {
            Camera.orthographicSize = gridSizeX > gridSizeY ? gridSizeX * 1.7f : gridSizeY * 1.7f;
            originalPos = transform.localPosition;
        }

        public void TriggerShake()
        {
            _ = Shake();
        }

        /// <summary>
        /// Camera Shake Effect
        /// </summary>
        private async Task Shake()
        {
            float elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
                elapsed += Time.deltaTime;
                await Task.Yield();
            }

            transform.localPosition = originalPos;
        }
    }

}