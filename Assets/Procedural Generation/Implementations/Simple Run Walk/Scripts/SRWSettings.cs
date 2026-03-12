using UnityEngine;

namespace ProcGen
{
    [CreateAssetMenu(fileName = "SRWSettings_", menuName = "ProcGen/Simple Random Walk Settings")]
    public class SRWSettings : ScriptableObject
    {
        public int iterations;
        public int walkLength;
        public bool startRandomStartingPositionEachIteration;
    }
}
