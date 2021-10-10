using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wildfire
{
    public class KeyboardController : MonoBehaviour
    {
        [Header("Move Settings")]
        public float moveSpeed = 2;
        public float zoomMultiplier = 1;
        
         Camera mainCamera;

         void Start() => mainCamera = Camera.main;
         
        private void Update()
        {
            Vector3 position = mainCamera.transform.position;
            mainCamera.transform.Translate(new Vector3(
                    moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime * (position.y * zoomMultiplier),
                    0,
                    moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * (position.y * zoomMultiplier)),
                Space.World);

            //This code is a placeholder for debugging - this should be part of the "Next Turn" sequence
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.AdvanceNextTurn();
            }

            //DebugTreeCount();
        }

        private static void DebugTreeCount()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("Getting tree counts for all tiles.");
                List<int> treeCounts = new List<int>();
                ProceduralTerrainTile[] tiles = FindObjectsOfType<ProceduralTerrainTile>();
                foreach (ProceduralTerrainTile ptt in tiles)
                {
                    int count = ptt.GetComponentsInChildren<Tree>().Length;
                    treeCounts.Add(count);
                }

                double average = treeCounts.Average();
                double total = treeCounts.Sum();
                double lowest = treeCounts.Min();
                double highest = treeCounts.Max();

                Debug.Log($"Counted a total of {total} trees across {treeCounts.Count} tiles.");
                Debug.Log($"The average tile has {average} trees.");
                Debug.Log($"The tile with the least amount of trees has {lowest}.");
                Debug.Log($"The tile with the most trees has {highest}.");

            }
        }
    }
}