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

        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            List<Vector2Int> corridor = new();
            corridor.Add(startPosition);
            
            var direction = Direction2D.GetRandomDirection();
            var currentPosition = startPosition;

            for (int i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition);
            }

            return corridor;
        }
    }
}
