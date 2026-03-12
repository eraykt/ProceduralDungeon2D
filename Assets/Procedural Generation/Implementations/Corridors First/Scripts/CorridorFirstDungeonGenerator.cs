using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProcGen
{
    public class CorridorFirstDungeonGenerator : SRWDungeonGenerator
    {
        [SerializeField] private int _corridorLength;
        [SerializeField] private int _corridorCount;
        [SerializeField, Range(0.1f, 1.0f)] private float _roomPercent;

        protected override void RunProceduralGeneration()
        {
            CorridorFirstGeneration();
        }

        private void CorridorFirstGeneration()
        {
            HashSet<Vector2Int> floorPositions = new();
            HashSet<Vector2Int> potantialRoomPositions = new();

            CreateCorridors(floorPositions, potantialRoomPositions);

            HashSet<Vector2Int> roomPositions = CreateRooms(potantialRoomPositions);

            List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
            CreateRoomsAtDeadEnds(deadEnds, roomPositions);

            floorPositions.UnionWith(roomPositions);

            _tilemapVisualizer.PaintFloorTiles(floorPositions);

            WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
        }

        private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
        {
            foreach (var position in deadEnds)
            {
                if (!roomFloors.Contains(position))
                {
                    var room = RunRandomWalk(_settings, position);
                    roomFloors.UnionWith(room);
                }
            }
        }

        private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> roomPositions)
        {
            List<Vector2Int> deadEnds = new();

            foreach (var position in roomPositions)
            {
                int neighbourCount = 0;

                foreach (var direction in Direction2D.directions)
                {
                    if (roomPositions.Contains(position + direction))
                    {
                        neighbourCount++;
                    }
                }

                if (neighbourCount == 1)
                {
                    deadEnds.Add(position);
                }
            }

            return deadEnds;
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potantialRoomPositions)
        {
            HashSet<Vector2Int> roomPositions = new();
            int roomToCreateCount = Mathf.RoundToInt(_roomPercent * potantialRoomPositions.Count);

            //TODO: Change this algorithm later. We need deterministic way.
            List<Vector2Int> roomsToCreate = potantialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

            foreach (var roomPosition in roomsToCreate)
            {
                var roomFloor = RunRandomWalk(_settings, roomPosition);
                roomPositions.UnionWith(roomFloor);
            }

            return roomPositions;
        }

        private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potantialRoomPositions)
        {
            var currentPosition = _startPosition;
            potantialRoomPositions.Add(currentPosition);

            for (int i = 0; i < _corridorCount; i++)
            {
                var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, _corridorLength);
                currentPosition = corridor[corridor.Count - 1];
                potantialRoomPositions.Add(currentPosition);
                floorPositions.UnionWith(corridor);
            }
        }
    }
}
