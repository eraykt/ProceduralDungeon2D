using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
    public static class WallGenerator
    {
        public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
        {
            HashSet<Vector2Int> basicWallPositions = FindWallsInDirection(floorPositions);

            foreach (var position in basicWallPositions)
            {
                tilemapVisualizer.PaintSingleBasicWall(position);
            }
        }

        private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPositions)
        {
            HashSet<Vector2Int> wallPositions = new();

            foreach (var position in floorPositions)
            {
                foreach (var directon in Direction2D.directions)
                {
                    var neighbourPosition = position + directon;
                    if (!floorPositions.Contains(neighbourPosition))
                    {
                        wallPositions.Add(neighbourPosition);
                    }
                }
            }

            return wallPositions;
        }
    }
}
