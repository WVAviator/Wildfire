using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class ProceduralTileInfo : MonoBehaviour
    {
        Mesh mesh;
        Vector3[] vertices;

        private void Start()
        {
            mesh = GetComponentInChildren<MeshFilter>().mesh;
            vertices = mesh.vertices;
        }

        public List<Vector3> GetWorldCoordinates()
        {
            List<Vector3> adjustedCoordinates = new List<Vector3>();
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].z < 0) continue;
                adjustedCoordinates.Add(GetWorldPosition(vertices[i]));
            }
            return adjustedCoordinates;
        }

        Vector3 GetWorldPosition(Vector3 vertex)
        {
            float actualX = (transform.position.x + (vertex.x * 100));
            float actualZ = transform.position.z - (vertex.y * 100);
            float actualY = (vertex.z * 100) + 0.25f;

            return new Vector3(actualX, actualY, actualZ);
        }
    }
}