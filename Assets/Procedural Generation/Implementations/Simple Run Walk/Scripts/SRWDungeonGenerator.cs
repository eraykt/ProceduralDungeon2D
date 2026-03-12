using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProcGen
{
    public class SRWDungeonGenerator : DungeonGeneratorBase
    {
        [SerializeField] protected SRWSettings _settings;

        protected override void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(_settings, _startPosition);

            _tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
        }

        protected HashSet<Vector2Int> RunRandomWalk(SRWSettings settings, Vector2Int position)
        {
            HashSet<Vector2Int> floorPositions = new();
            Vector2Int currentPosition = position;

            for (int i = 0; i < settings.iterations; i++)
            {
                var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, settings.walkLength);
                floorPositions.UnionWith(path);

                if (settings.startRandomStartingPositionEachIteration)
                {
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
                }
            }

            return floorPositions;
        }
    }
}
