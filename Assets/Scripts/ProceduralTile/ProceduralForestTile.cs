using UnityEngine;

namespace Wildfire
{
    public class ProceduralForestTile : MonoBehaviour
    {
        float noiseResolution;
        float xOffset;
        float yOffset;
        float density;
        int spacing;

        [SerializeField]
        GameObject[] treePrefabs;

        Mesh mesh;

        public void InitializeTile()
        {
            if (GetComponentInChildren<Tree>() != null) return;

            mesh = GetComponentInChildren<MeshFilter>().mesh;

            noiseResolution = ProceduralForestGenerator.Instance.NoiseResolution;
            xOffset = ProceduralForestGenerator.Instance.XOffset;
            yOffset = ProceduralForestGenerator.Instance.YOffset;
            density = ProceduralForestGenerator.Instance.Density;
            spacing = ProceduralForestGenerator.Instance.Spacing;

        
            SpawnForest();
        }

        public void ReplaceForest()
        {
            DeleteForest();
            noiseResolution = ProceduralForestGenerator.Instance.NoiseResolution;
            xOffset = ProceduralForestGenerator.Instance.XOffset;
            yOffset = ProceduralForestGenerator.Instance.YOffset;
            density = ProceduralForestGenerator.Instance.Density;
            spacing = ProceduralForestGenerator.Instance.Spacing;
            SpawnForest();
        }

        void DeleteForest()
        {
            Tree[] trees = GetComponentsInChildren<Tree>();
            if (trees.Length == 0) return;
            foreach (Tree t in trees) Destroy(t.gameObject);
        }

        void SpawnForest()
        {

            Vector3[] verts = GetVertices();

            for (int i = 0; i < verts.Length; i+=spacing)
            {
                if (verts[i].z < 0) continue;

                Vector3 worldPosition = GetWorldPosition(verts[i]);

                float perlinValue = GetNoiseValue(worldPosition.x, worldPosition.z);
                if (perlinValue < density) SpawnTree(worldPosition);
            }
        
        }

        Vector3 GetWorldPosition(Vector3 vertex)
        {
            Vector3 position = transform.position;
            float actualX = (position.x + (vertex.x * 100));
            float actualZ = position.z - (vertex.y * 100);
            float actualY = (vertex.z * 100);

            return new Vector3(actualX, actualY, actualZ);
        }

        Vector3[] GetVertices()
        {
            return mesh.vertices;
        }

        void SpawnTree(Vector3 position)
        {
            Instantiate(GetRandomTree(position), position, Quaternion.identity, transform);
        }

        GameObject GetRandomTree(Vector3 position)
        {
            Random.InitState((int)Mathf.RoundToInt(position.x * position.y * position.z * 2000));
            int random = Random.Range(0, treePrefabs.Length);
            return treePrefabs[random];
        }

        float GetNoiseValue(float x, float y)
        {
            float xCoord = (x + xOffset) * noiseResolution;
            float yCoord = (y + yOffset) * noiseResolution;

            float perlin = Mathf.PerlinNoise(xCoord, yCoord);

            return perlin;
        }

    }
}