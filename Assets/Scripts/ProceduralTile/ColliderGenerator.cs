using UnityEngine;

namespace Wildfire
{
    public class ColliderGenerator : MonoBehaviour
    {
        public void ConstructColliders()
        {
            MeshFilter mf = GetComponentInChildren<MeshFilter>();
            if (mf.gameObject.GetComponent<MeshCollider>() != null) return;
            MeshCollider mc = mf.gameObject.AddComponent<MeshCollider>();
            mc.sharedMesh = mf.sharedMesh;
        }
    }
}