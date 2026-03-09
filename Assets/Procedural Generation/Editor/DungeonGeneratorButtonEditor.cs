#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ProcGen
{
    [CustomEditor(typeof(DungeonGeneratorBase), true)]
    public class DungeonGeneratorButtonEditor : Editor
    {
        private DungeonGeneratorBase generator;

        private void Awake()
        {
            generator = (DungeonGeneratorBase)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Generate Dungeon"))
            {
                generator.GenerateDungeon();
            }

            if (GUILayout.Button("Clear Dungeon"))
            {
                generator.ClearDungeon();
            }
        }
    }
}
#endif
