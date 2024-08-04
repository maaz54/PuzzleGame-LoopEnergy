using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EnergyLoop.Game.Tiles.Details;
using EnergyLoop.Game.Tiles;
using ObjectPool.Interface;

namespace EnergyLoop.Game.Interface
{
    public class TileClickedEvent : UnityEvent<ITile> { }
    public interface ITile : IPoolableObject
    {
        // handle the callback when use click on tile
        TileClickedEvent OnTileClicked { get; set; }

        // tile transfrom
        new Transform Transform { get; }
        // tile position
        Vector3 Position { get; }

        TileData Data { get; }

        Node Node { get; }

        int CurrentRotationIndex { get; }

        bool IsConnectedWithPower { get; set; }

        //setting tile Details
        void SetTileDetails(TileData details, bool initializeNode);
        // setting tile position
        void SetPosition(Vector3 position);

        void SetMakeBGInvisible();

        void StartGlow();

        void StopGlowing();

        void SetType(TileType type);

        void RotateTile();
    }
}