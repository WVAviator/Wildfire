using System.Collections;
using UnityEngine;

namespace Wildfire
{
    public class TestGameSetup : MonoBehaviour
    {
    
    

        public HexTileMap hexTileMap;
        public bool EnableTestGame = true;

        [Header("Parameters for Testing")]
        public GameObject fire;
        public Vector2[] fireSpawns;
        [Space(10)]
        public GameObject engineUnit;
        public Vector2[] engineUnitSpawns;
        [Space(10)]
        public GameObject helicopterUnit;
        public Vector2[] helicopterUnitSpawns;

        void Start()
        {
            if (!EnableTestGame) return;

            foreach(Vector2 v in fireSpawns)
            {
                SpawnFire((int)v.x, (int)v.y);
            }

            foreach (Vector2 v in engineUnitSpawns)
            {
                SpawnUnit((int)v.x, (int)v.y, engineUnit);
            }

            foreach (Vector2 v in helicopterUnitSpawns)
            {
                SpawnUnit((int)v.x, (int)v.y, helicopterUnit);
            }
        }

        void SpawnFire(int x, int y)
        {
            HexTile startingHex = hexTileMap.FindHexTile(x, y);
            GameObject startingFire = Instantiate(fire, startingHex.transform.position, Quaternion.identity, startingHex.transform);
            //This is necessary because updating fire strength must occur after the fire's start method for some reason (without this it breaks)
            StartCoroutine(DelayUpdateFire(startingFire));
        }

        void SpawnUnit(int x, int y, GameObject unit)
        {
            HexTile startingHex = hexTileMap.FindHexTile(x, y);
            GameObject startingFire = Instantiate(unit, startingHex.transform.position, Quaternion.identity, startingHex.transform);
        }

        IEnumerator DelayUpdateFire(GameObject startingFire)
        {
            yield return new WaitForSeconds(1);

            startingFire.GetComponent<Fire>().UpdateFireStrength(12, false);
            yield return null;
        }

    }
}
