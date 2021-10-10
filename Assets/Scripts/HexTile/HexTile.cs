using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class HexTile : MonoBehaviour
    {

        Vector2 tileMapPosition;

        List<HexTile> neighbors;

        [HideInInspector] public SelectableTile Selection;


        private void Start()
        {
            neighbors = HexTileMap.Instance.GetNeighborsList(this);
            Selection = GetComponent<SelectableTile>();
        }

        public void SetHexCoordinates(Vector2 v)
        {
            tileMapPosition = v;
        }

        public List<HexTile> GetNeighbors()
        {
            return neighbors;
        }

        public Vector2 GetHexCoordinates()
        {
            return tileMapPosition;
        }

        public Vector3 GetXZWorldCoordinates()
        {
            Vector3 position = transform.position;
            return new Vector3(position.x, 0, position.z);
        }


    }
}