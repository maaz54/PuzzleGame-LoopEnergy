using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Manager
{
    public class CameraBehavior : MonoBehaviour
    {
        [SerializeField] Camera Camera;

        public void AdjustCameraSize(int gridSizeX, int gridSizeY)
        {
            Camera.orthographicSize = gridSizeX > gridSizeY ? gridSizeX + 2 : gridSizeY + 2;
            originalPos = transform.localPosition;
        }

        public float shakeDuration = 0.5f;
        public float shakeMagnitude = 0.1f;

        private Vector3 originalPos;

        [ContextMenu("TriggerShake")]
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