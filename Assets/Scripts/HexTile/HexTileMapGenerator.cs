using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class HexTileMapGenerator : MonoBehaviour
    {

        [Tooltip("Every tile on the map will initially be populated with this prefab.")]
        [SerializeField] GameObject blankHexPrefab;

        [Space(10)]

        [Tooltip("This sets the width of the map in number of tiles.")]
        public int mapWidth = 15;

        [Tooltip("This sets the height of the map in number of tiles.")]
        public int mapHeight = 15;

        [Space(10)]

        [Tooltip("This determines the spacing between tiles in Unity world space.")]
        [SerializeField] float tileSpacing = 0;
        
        public List<HexTile> GenerateHexMap()
        {
            List<HexTile> allHexes = new List<HexTile>();

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    GameObject instantiatedHex = Instantiate(blankHexPrefab,
                        GetWorldCoordinatesFromTileCoordinates(x, y), Quaternion.identity);
                    instantiatedHex.transform.parent = transform;
                    instantiatedHex.name = "TILE_" + x + "_" + y;
                    instantiatedHex.GetComponent<HexTile>().SetHexCoordinates(new Vector2(x, y));
                    allHexes.Add(instantiatedHex.GetComponent<HexTile>());
                }
            }
            return allHexes;
        }

        Vector3 GetWorldCoordinatesFromTileCoordinates(int xCoordinate, int yCoordinate)
        {
            float placementY = yCoordinate * TileMathConstants.VerticalTileOffset;

            float placementX = xCoordinate * 2 * TileMathConstants.HorizontalTileOffset;
            if (yCoordinate % 2 == 1) placementX += TileMathConstants.HorizontalTileOffset;

            placementY += yCoordinate * tileSpacing;
            placementX += xCoordinate * tileSpacing;
            if (yCoordinate % 2 == 1) placementX += 0.5f * tileSpacing;

            return new Vector3(placementX, 0, placementY);
        }
    }
}