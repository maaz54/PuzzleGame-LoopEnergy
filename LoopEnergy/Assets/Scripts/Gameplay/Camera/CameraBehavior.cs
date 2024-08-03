using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Manager
{
    public class CameraBehavior : MonoBehaviour
    {
        [SerializeField] Camera Camera;

        public void AdjustCameraSize(int gridSizeX, int gridSizeY)
        {
            Camera.orthographicSize = gridSizeX > gridSizeY ? gridSizeX + 2 : gridSizeY + 2;
        }
    }

}