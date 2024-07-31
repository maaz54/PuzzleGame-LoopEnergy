using EnergyLoop.Game.Interface;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using EnergyLoop.Game.Tiles.Details;
using System.Linq;

namespace EnergyLoop.Game.Tiles
{
    public class Tile : MonoBehaviour, ITile
    {
        /// <summary>
        /// handle the callback when use click on tile
        /// </summary>
        public TileClickedEvent OnTileClicked { get; set; } = new();
        [SerializeField] TileView[] tileView;
        [SerializeField] SpriteRenderer spriteRenderer;

        private Vector3 position;
        /// <summary>
        /// tile position
        /// </summary>
        public Vector3 Position => position;
        /// <summary>
        /// tile transfrom
        /// </summary>
        public Transform Transform => transform;

        private TileData data;

        public TileData Data => data;

        /// <summary>
        /// setting tile position
        /// </summary>
        public void SetPosition(Vector3 position)
        {
            this.position = position;
            transform.position = position;
        }

        public void SetTileDetails(TileData data)
        {
            this.data = data;
            transform.localEulerAngles = new Vector3(0,0,data.Properties.RotationZ); 
            SetType(this.data.Type);
        }

        public void SetType(TileType type)
        {
            spriteRenderer.sprite = tileView.First(view => view.Type == type).sprite;
            spriteRenderer.size = new Vector2(1, 1);
        }

        /// <summary>
        /// tile transfrom
        /// </summary>
        private void OnMouseDown()
        {
            transform.localScale = Vector3.one * 1.1f;
            OnTileClicked?.Invoke(this);

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