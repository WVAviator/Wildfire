using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wildfire
{
    public class WorldTile : MonoBehaviour
    {
        List<Tree> trees;
        float startingTreeCount;
        float startingFuel;
        float currentFuel;

        float flammability;

        HexTile hex;

        static readonly List<WorldTile> AllWorldTiles = new List<WorldTile>();
        void OnEnable() => AllWorldTiles.Add(this);
        void OnDisable() => AllWorldTiles.Remove(this);
        public static WorldTile FindWorldTile(HexTile h)
        {
            return AllWorldTiles.FirstOrDefault(wt => wt.GetHex() == h);
        }

        private void Awake()
        {
            hex = transform.parent.GetComponent<HexTile>();
        }

        private void Start()
        {
            GetTrees();
            EstablishTileHealth();

            flammability = startingTreeCount / 20; //TODO: Find a way to not hard code this, make it configurable?
        }
        private void GetTrees()
        {
            trees = new List<Tree>(GetComponentsInChildren<Tree>());
            trees.Shuffle();
            startingTreeCount = trees.Count;
        }

        private void EstablishTileHealth()
        {
            startingFuel = (trees.Count * 3) + 20;
            currentFuel = startingFuel;
        }
        public void DamageTile(float damage)
        {
            currentFuel -= damage;

            float percentFuelRemaining = currentFuel / startingFuel;
            float percentTreesRemaining = trees.Count / startingTreeCount;
            while (percentFuelRemaining < percentTreesRemaining)
            {
                trees[0].BurnDown();
                trees.RemoveAt(0);
                percentTreesRemaining = trees.Count / startingTreeCount;
            }
        }

        internal float GetFlammability()
        {
            return flammability;
        }
        internal float GetRemainingFuel()
        {
            return currentFuel;
        }

        HexTile GetHex()
        {
            return hex;
        }





    }
}