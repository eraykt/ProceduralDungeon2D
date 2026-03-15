using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProcGen
{
    public class RoomFirstDungeonGenerator : SRWDungeonGenerator
    {
        [SerializeField] private Vector2Int _minRoomSize;
        [SerializeField] private Vector2Int _minDungeonSize;
        [SerializeField, Range(0, 10)] private int _offset;
        [SerializeField] private bool _randomWalkRooms;

        protected override void RunProceduralGeneration()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
            var roomList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(
                new BoundsInt(
                    (Vector3Int)_startPosition,
                    (Vector3Int)_minDungeonSize
                ),
                _minRoomSize.x,
                _minRoomSize.y
            );

            HashSet<Vector2Int> floor = new();
            
            floor = _randomWalkRooms ? CreateRoomsRandomly(roomList) : CreateSimpleRooms(roomList);

            List<Vector2Int> roomCenters = new();
            foreach (var room in roomList)
            {
                roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            }

            HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

            floor.UnionWith(corridors);

            _tilemapVisualizer.PaintFloorTiles(floor);
            WallGenerator.CreateWalls(floor, _tilemapVisualizer);
        }

        private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
        {
            HashSet<Vector2Int> floor = new();
            for (int i = 0; i < roomList.Count; i++)
            {
                var roomBounds = roomList[i];
                var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
                var roomFloor = RunRandomWalk(_settings, roomCenter);

                foreach (var position in roomFloor)
                {
                    if (position.x >= (roomBounds.xMin + _offset) && position.x <= (roomBounds.xMax - _offset) 
                        && position.y >= (roomBounds.yMin + _offset) && position.y <= (roomBounds.yMax - _offset))
                    {
                        floor.Add(position);
                    }
                }
            }

            return floor;
        }

        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            HashSet<Vector2Int> corridors = new();
            var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            while (roomCenters.Count > 0)
            {
                Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closest);
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }

        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
            var position = currentRoomCenter;
            corridor.Add(position);

            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                }
                else if (destination.y < position.y)
                {
                    position += Vector2Int.down;
                }
                corridor.Add(position);
            }
            
            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }
                else if (destination.x < position.x)
                {
                    position += Vector2Int.left;
                }
                corridor.Add(position);
            }
            return corridor;
        }

        private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            Vector2Int closest = Vector2Int.zero;
            float distance = float.MaxValue;

            foreach (var position in roomCenters)
            {
                float currentDistance = Vector2.Distance(position, currentRoomCenter);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = position;
                }
            }

            return closest;
        }

        private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
        {
            HashSet<Vector2Int> floor = new();
            foreach (var room in roomList)
            {
                for (int col = _offset; col < room.size.x - _offset; col++)
                {
                    for (int row = _offset; row < room.size.y - _offset; row++)
                    {
                        Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                        floor.Add(position);
                    }
                }
            }

            return floor;
        }
    }
}
