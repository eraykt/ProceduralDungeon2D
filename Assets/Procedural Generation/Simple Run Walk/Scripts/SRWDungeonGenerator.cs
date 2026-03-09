using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProcGen
{
    public class SRWDungeonGenerator : DungeonGeneratorBase
    {
        [SerializeField] private SRWSettings _settings;

        protected override void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk();

            _tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
        }

        private HashSet<Vector2Int> RunRandomWalk()
        {
            HashSet<Vector2Int> floorPositions = new();
            Vector2Int currentPosition = _startPosition;

            for (int i = 0; i < _settings.iterations; i++)
            {
                var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, _settings.walkLength);
                floorPositions.UnionWith(path);

                if (_settings.startRandomStartingPositionEachIteration)
                {
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
                }
            }

            return floorPositions;
        }
    }
}
