using UnityEngine;

namespace Wildfire
{
    public class ProceduralForestGenerator : MonoBehaviour
    {
        [SerializeField] bool liveUpdate;
        [Range(0.01f, 3)]
        public float NoiseResolution = 0.36f;
        public float XOffset = 0;
        public float YOffset = 0;
        [Range(0,1)]
        public float Density = 0.5f;
        [Range(10, 100)]
        public int Spacing = 20;

        public static ProceduralForestGenerator Instance;

        ProceduralForestTile[] tiles;

        private void Awake() => Instance = this;
        private void Start() => tiles = FindObjectsOfType<ProceduralForestTile>();
        private void Update()
        {
            if (!liveUpdate) return;
            if (tiles.Length == 0) tiles = FindObjectsOfType<ProceduralForestTile>();
            foreach (ProceduralForestTile tile in tiles)
            {
                tile.ReplaceForest();
            }
        }

    }
}