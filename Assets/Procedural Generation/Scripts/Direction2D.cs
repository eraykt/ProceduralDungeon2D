using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
    public static class Direction2D
    {
        public static List<Vector2Int> directions = new()
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

        public static List<Vector2Int> diagonalDirections = new()
        {
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1)
        };

        public static List<Vector2Int> eightDirections = new List<Vector2Int>
        {
            Vector2Int.up,
            new Vector2Int(1, 1),
            Vector2Int.right,
            new Vector2Int(1, -1),
            Vector2Int.down,
            new Vector2Int(-1, -1),
            Vector2Int.left,
            new Vector2Int(-1, 1)
        };

        public static Vector2Int GetRandomDirection()
        {
            return directions[Random.Range(0, directions.Count)];
        }
    }
}
