using UnityEngine;

namespace Wildfire
{
    public class TerrainHugger : MonoBehaviour
    {
        [SerializeField] Transform footPoint;
        [SerializeField] bool matchTerrainRotation = true;

        private void Start() => HugTerrain();
        public void HugTerrain()
        {
            Vector3 position = footPoint.position;
            Ray ray = new Ray(new Vector3(position.x, 10, position.z), Vector3.down);
            if(!Physics.Raycast(ray, out RaycastHit hitInfo, 12, LayerMask.GetMask("HexTile"))) return;

            transform.position = hitInfo.point + (transform.position - footPoint.position);
            if (matchTerrainRotation) transform.rotation = Quaternion.FromToRotation(transform.up, hitInfo.normal) * transform.rotation;

        }

  

    }
}