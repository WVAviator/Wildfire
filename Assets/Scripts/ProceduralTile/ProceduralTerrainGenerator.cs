using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class ProceduralTerrainGenerator : MonoBehaviour
    {
        List<ProceduralTerrainTile> tiles = new List<ProceduralTerrainTile>();

        [SerializeField] bool liveUpdating;

        [SerializeField] GameObject terrainPrefab;
        
        [Space(10)]
        
        [Range(0.01f, 3)]
        [SerializeField] float noiseResolution = 1;
        [SerializeField] float xOffset = 0;
        [SerializeField] float yOffset = 0;
    
        [Range(0.0001f, 0.1f)]
        [SerializeField] float height = 0.01f;

        private void Awake()
        {
            GenerateTerrainWorldTileMap();
        }

        private void Update()
        {
            if (!liveUpdating) return;
            foreach (ProceduralTerrainTile t in tiles) t.InitializeTile(noiseResolution, xOffset, yOffset, height);
        }

        void GenerateTerrainWorldTileMap()
        {
            foreach (HexTile hex in HexTileMap.Instance.GetAllHexTiles())
            {
                GameObject newWorldTile = Instantiate(terrainPrefab, hex.transform.position, Quaternion.identity, hex.transform);
                ProceduralTerrainTile terrainTile = newWorldTile.GetComponent<ProceduralTerrainTile>();
                tiles.Add(terrainTile);

                terrainTile.InitializeTile(noiseResolution, xOffset, yOffset, height);
            
            }
        }

    }
}