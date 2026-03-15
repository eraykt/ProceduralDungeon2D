using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ProcGen
{
    public class TilemapVisualizer : MonoBehaviour
    {
        [Header("Floor References")]
        [SerializeField] private Tilemap _floorMap;
        [SerializeField] private TileBase _floorBaseTile;

        [Header("Wall References")]
        [SerializeField] private Tilemap _wallMap;
        [SerializeField] private TileBase _wallTop;
        [SerializeField] private TileBase _wallSideRight;
        [SerializeField] private TileBase _wallSideLeft;
        [SerializeField] private TileBase _wallBottom;
        [SerializeField] private TileBase _wallFull;
        [SerializeField] private TileBase _wallInnerCornerDownLeft;
        [SerializeField] private TileBase _wallInnerCornerDownRight;
        [SerializeField] private TileBase _wallDiagonalCornerDownLeft;
        [SerializeField] private TileBase _wallDiagonalCornerDownRight;
        [SerializeField] private TileBase _wallDiagonalCornerUpRight;
        [SerializeField] private TileBase _wallDiagonalCornerUpLeft;

        public void PaintFloorTiles(IEnumerable<Vector2Int> positions)
        {
            PaintTiles(positions, _floorMap, _floorBaseTile);
        }

        public void PaintSingleBasicWall(Vector2Int position, string neigbhbourBinaryType)
        {
            int typeAsInt = Convert.ToInt32(neigbhbourBinaryType, 2);
            TileBase tile = null;

            if (WallTypesHelper.wallTop.Contains(typeAsInt))
            {
                tile = _wallTop;
            }
            else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
            {
                tile = _wallSideRight;

            }
            else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
            {
                tile = _wallSideLeft;
            }
            else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
            {
                tile = _wallBottom;
            }
            else if (WallTypesHelper.wallFull.Contains(typeAsInt))
            {
                tile = _wallFull;
            }

            if (tile)
            {
                PaintSingleTile(position, _wallMap, tile);
            }
        }

        private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase baseTile)
        {
            foreach (var position in positions)
            {
                PaintSingleTile(position, tilemap, baseTile);
            }
        }

        private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase baseTile)
        {
            var worldPosition = tilemap.WorldToCell((Vector3Int)position);
            tilemap.SetTile(worldPosition, baseTile);
        }

        public void Clear()
        {
            _floorMap.ClearAllTiles();
            _wallMap.ClearAllTiles();
        }

        public void PaintSingleCornerWall(Vector2Int position, string neigbhbourBinaryType)
        {
            int typeAsInt = Convert.ToInt32(neigbhbourBinaryType, 2);
            TileBase tile = null;

            if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
            {
                tile = _wallInnerCornerDownLeft;
            }
            else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
            {
                tile = _wallInnerCornerDownRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
            {
                tile = _wallDiagonalCornerDownLeft;
            }
            else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
            {
                tile = _wallDiagonalCornerDownRight;
            }
            else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
            {
                tile = _wallDiagonalCornerUpLeft;
            }
            else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
            {
                tile = _wallDiagonalCornerUpRight;
            }
            else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
            {
                tile = _wallFull;
            }
            else if (WallTypesHelper.wallBottomEightDirections.Contains(typeAsInt))
            {
                tile = _wallBottom;
            }

            if (tile)
            {
                PaintSingleTile(position, _wallMap, tile);
            }
        }
    }
}
