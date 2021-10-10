using UnityEngine;

namespace Wildfire
{
    public class ProceduralTerrainTile : MonoBehaviour
    {
        float noiseResolution;
        float xOffset;
        float yOffset;
        float height;

        MeshFilter meshFilter;
        Mesh mesh;
 

        public void InitializeTile(float noiseResolution, float xOffset, float yOffset, float height)
        {
            meshFilter = GetComponentInChildren<MeshFilter>();
            mesh = meshFilter.mesh;

            this.noiseResolution = noiseResolution;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.height = height;

            MorphTerrain();
        }
        
        void MorphTerrain()
        {

            Vector3[] verts = GetVertices();
            Vector3[] newVerts = new Vector3[verts.Length];

            for (int i = 0; i < verts.Length; i++)
            {
                if (verts[i].z < 0) //Because blender, z is the axis that will control height
                {
                    newVerts[i] = verts[i];
                    continue;
                }

                Vector3 position = transform.position;
                float sampleX = -(position.x + (verts[i].x * 100));
                float sampleY = position.z - (verts[i].y * 100);

                newVerts[i] = new Vector3(verts[i].x, verts[i].y, GetNoiseValue(sampleX, sampleY));
            }

            SetVertices(newVerts);

            FinishTileSetup();
        }

        void FinishTileSetup()
        {
            GetComponent<HoverShroud>().InitializeShroud(meshFilter.gameObject);
            GetComponent<ColliderGenerator>().ConstructColliders();
            GetComponent<ProceduralForestTile>().InitializeTile();
        }

        Vector3[] GetVertices()
        {
            return mesh.vertices;
        }

        void SetVertices(Vector3[] verts)
        {
            mesh.vertices = verts;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
        }

        float GetNoiseValue(float x, float y)
        {
            float xCoord = (x + xOffset) * noiseResolution;
            float yCoord = (y + yOffset) * noiseResolution;

            float perlin = Mathf.PerlinNoise(xCoord, yCoord);
            
            float rise = height * perlin;

            return rise;
        }
    }
}