using Puzzle.Match.Interface;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Puzzle.Match.Tiles
{
    public class Tile : MonoBehaviour, ITile
    {
        /// <summary>
        /// handle the callback when use click on tile
        /// </summary>
        public TileClickedEvent OnTileSelect { get; set; } = new();
        [SerializeField] int tileNo;
        /// <summary>
        /// Unique tile identifier
        /// </summary>
        public int TileNo => tileNo;
        TileIndex index;
        /// <summary>
        /// Tile index in a grid
        /// </summary>
        public TileIndex Index => index;
        private Vector3 position;
        /// <summary>
        /// tile position
        /// </summary>
        public Vector3 Position => position;
        /// <summary>
        /// tile transfrom
        /// </summary>
        public Transform Transform => transform;

        public int ObjectID => tileNo;


        /// <summary>
        /// setting tile index
        /// </summary>
        public void SetIndex(TileIndex index)
        {
            this.index = index;
        }

        /// <summary>
        /// setting tile position
        /// </summary>
        public void SetPosition(Vector3 position)
        {
            this.position = position;
            transform.position = position;
        }

        /// <summary>
        /// tile transfrom
        /// </summary>
        private void OnMouseDown()
        {
            transform.localScale = Vector3.one * 1.1f;
            OnTileSelect?.Invoke(this);
        }
        /// <summary>
        /// tile transfrom
        /// </summary>
        private void OnMouseUp()
        {
            transform.localScale = Vector3.one;
        }
        /// <summary>
        /// destroying tile
        /// </summary>
        public void DestroyTile()
        {
           Destroy(gameObject);
        }
    }
}