using UnityEngine;

namespace ProcGen
{
    public abstract class DungeonGeneratorBase : MonoBehaviour
    {
        [SerializeField] protected TilemapVisualizer _tilemapVisualizer;
        [SerializeField] protected Vector2Int _startPosition;

        public void GenerateDungeon()
        {
            _tilemapVisualizer.Clear();
            RunProceduralGeneration();
        }

        public void ClearDungeon()
        {
            _tilemapVisualizer.Clear();
        }

        protected abstract void RunProceduralGeneration();
    }
}
