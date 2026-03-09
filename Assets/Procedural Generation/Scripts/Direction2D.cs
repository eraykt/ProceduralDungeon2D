using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
    public static class Direction2D
    {
        public static List<Vector2Int> directions = new()
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        public static Vector2Int GetRandomDirection()
        {
            return directions[Random.Range(0, directions.Count)];
        }
    }
}
