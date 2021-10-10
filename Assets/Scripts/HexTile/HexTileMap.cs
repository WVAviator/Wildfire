using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wildfire
{
    public class HexTileMap : MonoBehaviour
    {
        int mapWidth;
        int mapHeight;
        HexTileMapGenerator mapGenerator;
        public static HexTileMap Instance;
        HexTile[] allHexTiles;
        private void Awake()
        {
            Instance = this;
        
            mapGenerator = GetComponent<HexTileMapGenerator>();
            mapWidth = mapGenerator.mapWidth;
            mapHeight = mapGenerator.mapHeight;

            allHexTiles = mapGenerator.GenerateHexMap().ToArray();
        }

        public void ShroudUnselectables(List<HexTile> selectables)
        {
            foreach (HexTile hex in allHexTiles)
            {
                if (selectables.Contains(hex)) continue;
                hex.GetComponentInChildren<HoverShroud>().ShowTileUnselectable();
            }
        }
        public void UnShroudAllTiles()
        {
            foreach (HexTile hex in allHexTiles)
            {
                hex.GetComponentInChildren<HoverShroud>().ShowTileSelectable();
            }
        }

        public Vector3[] GetMapCorners()
        {
            Vector3[] mapCorners = new Vector3[4];

            float horizontalTileOffset = TileMathConstants.HorizontalTileOffset;
            float verticalTileOffset = TileMathConstants.VerticalTileOffset;

            //Southwest corner (the left edge is negative one horizontal tile offset);
            mapCorners[0] = new Vector3(-horizontalTileOffset, 0, 0);

            //Northwest corner (map height times y offset)
            mapCorners[1] = new Vector3(-horizontalTileOffset, 0, verticalTileOffset * mapHeight);

            //Northeast corner (map height times y offset for z, map width times x offset for x)
            mapCorners[2] = new Vector3(mapWidth * horizontalTileOffset * 2, 0, verticalTileOffset* mapHeight); //Must add back the offset to account for offset rows

            //Southeast corner (map width times offset for x)
            mapCorners[3] = new Vector3(mapWidth * horizontalTileOffset * 2, 0, 0);

            return mapCorners;
        }
    
        public static HexTile FindHexTile(Vector3 worldCoordinates)
        {
            Ray ray = new Ray(new Vector3(worldCoordinates.x, 2.5f, worldCoordinates.z), Vector3.down);

            //Raycast seemed like the least mathy way to do this
            Physics.Raycast(ray, out RaycastHit hitInfo, 5, LayerMask.GetMask("HexTile"));
            HexTile hex = hitInfo.transform.parent.parent.GetComponent<HexTile>();
            return hex;
        }

        HexTile FindHexTile(Vector2 hexCoordinates)
        {
            IEnumerable<HexTile> hexTiles = GetAllHexTiles();
            return hexTiles.FirstOrDefault(hex => hexCoordinates == hex.GetHexCoordinates());
        }
        public HexTile FindHexTile(int x, int y)
        {
            return FindHexTile(new Vector2(x, y));
        }

        public IEnumerable<HexTile> GetAllHexTiles()
        {
            return allHexTiles;
        }

        public HexTile[] GetNeighbors(HexTile hexTile)
        {
            List<HexTile> neighbors = new List<HexTile>();

            Vector2 hexCoords = hexTile.GetHexCoordinates();

            bool isOddRow = hexCoords.y % 2 == 1;

        
            //Northwest on even row
            if (!isOddRow && hexCoords.x > 0 && hexCoords.y < mapHeight - 1) neighbors.Add(FindHexTile(new Vector2(hexCoords.x - 1, hexCoords.y + 1)));
            //Northwest on odd row
            if (isOddRow && hexCoords.y < mapHeight - 1) neighbors.Add(FindHexTile(new Vector2(hexCoords.x + 0, hexCoords.y + 1)));

            //Southwest on even row
            if (!isOddRow && hexCoords.x > 0 && hexCoords.y > 0) neighbors.Add(FindHexTile(new Vector2(hexCoords.x - 1, hexCoords.y - 1)));
            //Southwest on odd row
            if (isOddRow && hexCoords.y > 0) neighbors.Add(FindHexTile(new Vector2(hexCoords.x - 0, hexCoords.y - 1)));

            //West
            if (hexCoords.x > 0) neighbors.Add(FindHexTile(new Vector2(hexCoords.x - 1, hexCoords.y - 0)));

            //Northeast on even row
            if (!isOddRow && hexCoords.y < mapHeight - 1) neighbors.Add(FindHexTile(new Vector2(hexCoords.x + 0, hexCoords.y + 1)));
            //Northeast on odd row
            if (isOddRow && hexCoords.y < mapHeight - 1 && hexCoords.x < mapWidth - 1) neighbors.Add(FindHexTile(new Vector2(hexCoords.x + 1, hexCoords.y + 1)));

            //Southeast on even row
            if (!isOddRow && hexCoords.y > 0) neighbors.Add(FindHexTile(new Vector2(hexCoords.x - 0, hexCoords.y - 1)));
            //Southeast on odd row
            if (isOddRow && hexCoords.y > 0 && hexCoords.x < mapWidth - 1) neighbors.Add(FindHexTile(new Vector2(hexCoords.x + 1, hexCoords.y - 1)));

            //East
            if (hexCoords.x < mapWidth - 1) neighbors.Add(FindHexTile(new Vector2(hexCoords.x + 1, hexCoords.y - 0)));

            return neighbors.ToArray();
        }

        public List<HexTile> GetNeighborsList(HexTile hex)
        {
            return new List<HexTile>(GetNeighbors(hex));
        }

        public int GetHeadingBetween(HexTile hexFrom, HexTile hexTo)
        {
        
            bool isOddRow = (int)hexFrom.GetHexCoordinates().y % 2 == 1;

            int xDiff = (int)(hexTo.GetHexCoordinates().x - hexFrom.GetHexCoordinates().x);
            int yDiff = (int)(hexTo.GetHexCoordinates().y - hexFrom.GetHexCoordinates().y);

            if (xDiff == 1 && yDiff == 0) return 90;
            if (xDiff == -1 && yDiff == 0) return 270;

            if (isOddRow)
            {
                if (xDiff == 1 && yDiff == 1) return 30;
                if (xDiff == 1 && yDiff == -1) return 150;
                if (xDiff == 0 && yDiff == -1) return 210;
                if (xDiff == 0 && yDiff == 1) return 330;
            }
            if (!isOddRow)
            {
                if (xDiff == 0 && yDiff == 1) return 30;
                if (xDiff == 0 && yDiff == -1) return 150;
                if (xDiff == -1 && yDiff == -1) return 210;
                if (xDiff == -1 && yDiff == 1) return 330;
            }
            Debug.LogError("An attempt to return the angle between two tiles failed because the tiles are not adjacent.");
            return 90;
        }

        public HexTile GetTileInDirection(HexTile hex, Direction dir)
        {
            int x = (int)hex.GetHexCoordinates().x;
            int y = (int)hex.GetHexCoordinates().y;

            bool isOddRow = (int)hex.GetHexCoordinates().y % 2 == 1;

            if (dir == Direction.E) return FindHexTile(x + 1, y);
            if (dir == Direction.W) return FindHexTile(x - 1, y);

            if (isOddRow)
            {
                if (dir == Direction.NW) return FindHexTile(x, y + 1);
                if (dir == Direction.NE) return FindHexTile(x + 1, y + 1);
                if (dir == Direction.SE) return FindHexTile(x + 1, y - 1);
                if (dir == Direction.SW) return FindHexTile(x, y - 1);
            }
            if (!isOddRow)
            {
                if (dir == Direction.NW) return FindHexTile(x - 1, y + 1);
                if (dir == Direction.NE) return FindHexTile(x, y + 1);
                if (dir == Direction.SE) return FindHexTile(x , y - 1);
                if (dir == Direction.SW) return FindHexTile(x - 1, y - 1);
            }

            return null;

        }

        public void DeselectAllTiles()
        {
            foreach (HexTile hex in GetAllHexTiles())
            {
                if (hex.Selection.IsSelected()) hex.Selection.ToggleSelection();
            }
        }

        public List<HexTile> GetHexTilesInRange(HexTile hex, int range)
        {
            HashSet<HexTile> tiles = new HashSet<HexTile> {hex};

            for (int i = 0; i < range; i++)
            {
                HashSet<HexTile> newNeighbors = new HashSet<HexTile>();
                foreach (HexTile h in tiles)
                {
                    newNeighbors.UnionWith(GetNeighborsList(h));
                }
                tiles.UnionWith(newNeighbors);
            }
            return new List<HexTile>(tiles);
        }

    }
}