using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
    public static class WallGenerator
    {
        public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
        {
            HashSet<Vector2Int> basicWallPositions = FindWallsInDirection(floorPositions, Direction2D.directions);
            HashSet<Vector2Int> cornerWallPositions = FindWallsInDirection(floorPositions, Direction2D.diagonalDirections);

            CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
            CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
        }

        private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
        {
            foreach (var position in cornerWallPositions)
            {
                string neigbhbourBinaryType = "";

                foreach (var direction in Direction2D.eightDirections)
                {
                    var neighbourPosition = position + direction;
                    if (floorPositions.Contains(neighbourPosition))
                    {
                        neigbhbourBinaryType += "1";
                    }
                    else
                    {
                        neigbhbourBinaryType += "0";
                    }
                }

                tilemapVisualizer.PaintSingleCornerWall(position, neigbhbourBinaryType);
            }
        }

        private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
        {
            foreach (var position in basicWallPositions)
            {
                string neigbhbourBinaryType = "";

                foreach (var direction in Direction2D.directions)
                {
                    var neighbourPosition = position + direction;
                    if (floorPositions.Contains(neighbourPosition))
                    {
                        neigbhbourBinaryType += "1";
                    }
                    else
                    {
                        neigbhbourBinaryType += "0";
                    }
                }

                tilemapVisualizer.PaintSingleBasicWall(position, neigbhbourBinaryType);
            }
        }

        private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
        {
            HashSet<Vector2Int> wallPositions = new();

            foreach (var position in floorPositions)
            {
                foreach (var directon in directionsList)
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
