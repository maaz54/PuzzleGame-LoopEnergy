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
        [SerializeField] SpriteRenderer spriteTileType;

        [SerializeField] SpriteRenderer BgSprite;
        [SerializeField] Color glowColor;

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

        int[] rotationValues = { 360, 270, 180, 90, };

        private Node node;

        public Node Node => node;

        int currentRotationIndex;
        public int CurrentRotationIndex => currentRotationIndex;

        public bool IsConnectedWithPower { get; set; }

        /// <summary>
        /// setting tile position
        /// </summary>
        public void SetPosition(Vector3 position)
        {
            this.position = position;
            transform.position = position;
        }

        public void SetMakeBGInvisible()
        {
            BgSprite.enabled = false;
        }

        public void StartGlow()
        {
            spriteTileType.color = glowColor;
            IsConnectedWithPower  = true;
        }

        public void StopGlowing()
        {
            spriteTileType.color = Color.white;
            IsConnectedWithPower  = false;
        }

        public void SetTileDetails(TileData data)
        {
            this.data = data;
            SetType(this.data.Type);
            InitializeNode();
        }

        private void InitializeNode()
        {
            StopGlowing();
            if (data.Type == TileType.None)
            {
                node = new Node(0, 0, 0, 0);
            }
            else if (data.Type == TileType.Power)
            {
                IsConnectedWithPower = true;
                StartGlow();
                node = new Node(1, 1, 1, 1);
            }
            else if (data.Type == TileType.Bulb)
            {
                node = new Node(0, 0, 1, 0);
            }
            else if (data.Type == TileType.Wire1)
            {
                node = new Node(0, 1, 0, 1);
            }
            else if (data.Type == TileType.Wire2)
            {
                node = new Node(1, 0, 0, 1);
            }
        }

        public void RotateTile()
        {
            currentRotationIndex++;
            if (currentRotationIndex >= rotationValues.Length)
            {
                currentRotationIndex = 0;
            }
            SetZRotation(currentRotationIndex);
            node.RotateClockwise();
        }

        public void SetZRotation(int index)
        {
            transform.localEulerAngles = new Vector3(0, 0, rotationValues[index]);
            currentRotationIndex = index;
        }

        public void SetType(TileType type)
        {
            spriteTileType.sprite = tileView.First(view => view.Type == type).sprite;
            spriteTileType.size = new Vector2(1, 1);
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