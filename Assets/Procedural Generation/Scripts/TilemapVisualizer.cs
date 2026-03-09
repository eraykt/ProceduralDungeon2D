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
        [SerializeField] private TileBase _wallBaseTile;

        public void PaintFloorTiles(IEnumerable<Vector2Int> positions)
        {
            PaintTiles(positions, _floorMap, _floorBaseTile);
        }
        
        public void PaintSingleBasicWall(Vector2Int position)
        {
            PaintSingleTile(position, _wallMap, _wallBaseTile);
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
    }
}
