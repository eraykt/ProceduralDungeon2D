using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
    public static class ProceduralGenerationAlgorithms
    {
        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
        {
            HashSet<Vector2Int> path = new();

            path.Add(startPosition);
            Vector2Int previousPosition = startPosition;

            for (int i = 0; i < walkLength; i++)
            {
                Vector2Int newPosition = previousPosition + Direction2D.GetRandomDirection();
                path.Add(newPosition);
                previousPosition = newPosition;
            }

            return path;
        }
    }
}
    