using UnityEngine;

namespace Wildfire
{
    public class HexTileInfo : MonoBehaviour
    {
        HexTile hex;
        [HideInInspector] public ProceduralForestTile ProceduralForestTile;
        [HideInInspector] public ProceduralTerrainTile ProceduralTerrainTile;
        [HideInInspector] public ProceduralTileInfo ProceduralTileInfo;

        void Awake()
        {
            hex = GetComponent<HexTile>();
        }
        void Start()
        {
            ProceduralTerrainTile = hex.GetComponentInChildren<ProceduralTerrainTile>();
            ProceduralForestTile = hex.GetComponentInChildren<ProceduralForestTile>();
            ProceduralTileInfo = hex.GetComponentInChildren<ProceduralTileInfo>();
        }

        public void SetVertices(Vector3[] allVertices)
        {

        }
        public Vector3 GetCenterOfTile()
        {
            Vector3 position = hex.transform.position;
            Ray ray = new Ray(new Vector3(position.x, 10, position.z), Vector3.down);
            Physics.Raycast(ray, out RaycastHit hit, 12, LayerMask.GetMask("HexTile"));
            return hit.point;
        }
    }
}