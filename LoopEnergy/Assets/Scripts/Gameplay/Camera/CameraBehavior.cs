using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Manager
{
    public class CameraBehavior : MonoBehaviour
    {
        [SerializeField] Camera Camera;

        [SerializeField] float shakeDuration = 0.5f;
        [SerializeField] float shakeMagnitude = 0.1f;
        private Vector3 originalPos;

        public void AdjustCameraSize(int gridSizeX, int gridSizeY)
        {
            Camera.orthographicSize = gridSizeX > gridSizeY ? gridSizeX * 1.25f : gridSizeY * 1.25f;
            originalPos = transform.localPosition;
        }

        public void TriggerShake()
        {
            _ = Shake();
        }

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