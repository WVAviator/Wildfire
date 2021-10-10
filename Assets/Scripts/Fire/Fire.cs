using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class Fire : MonoBehaviour
    {
        WorldTile worldTile;
        FireAppearance fireAppearance;
        HexTile currentHex;
        HexTile[] neighbors;

        [Tooltip("Starting fire strength")]
        [SerializeField] float fireStrength = 4;

        [Tooltip("Maximum Fire Strength")] 
        static float MaxFireStrength = 20;

        [SerializeField] Fire firePrefab;

        public static event Action OnAllFiresExtinguished = delegate { };

        static List<Fire> AllFires = new List<Fire>();

        void OnEnable() => AllFires.Add(this);

        void OnDisable()
        {
            AllFires.Remove(this);
            if (AllFires.Count == 0) OnAllFiresExtinguished?.Invoke();
        }
        public void Start()
        {
            currentHex = transform.parent.GetComponent<HexTile>();
            neighbors = HexTileMap.Instance.GetNeighbors(currentHex);
            
            GameManager.OnAdvanceNextTurn += UpdateFireNewTurn;
            
            worldTile = transform.parent.GetComponentInChildren<WorldTile>();

            fireAppearance = GetComponent<FireAppearance>();
            fireAppearance.UpdateFireAppearance(fireStrength);
        }

        void UpdateFireNewTurn()
        {
            if (this == null) return;
            foreach (HexTile potentialSpreadTile in neighbors)
            {
                //Make sure the tile in question doesn't already have a fire
                if (potentialSpreadTile.GetComponentInChildren<Fire>() != null) continue;
                SpawnFire(potentialSpreadTile, FireFactor.GetFireSpreadFactor(currentHex, potentialSpreadTile));
            }
            UpdateFireStrength(FireFactor.GetFireGrowthPerTurn(currentHex), true);
        }
        
        void SpawnFire(HexTile potentialSpreadTile, float probability)
        {
            float rnd = UnityEngine.Random.Range(0f, 1f);

            if (rnd < probability)
                Instantiate(firePrefab, potentialSpreadTile.transform.position, Quaternion.identity, potentialSpreadTile.transform);
        }
        
        public static float GetMaxFireStrength()
        {
            return MaxFireStrength;
        }
        public void UpdateFireStrength(float fireStrengthAdjustment, bool burnFuel)
        {
            fireStrength += fireStrengthAdjustment;
            if (fireStrength > MaxFireStrength) fireStrength = MaxFireStrength;
            if (fireStrength > worldTile.GetRemainingFuel()) fireStrength = worldTile.GetRemainingFuel();
            if (fireStrength <= 0)
            {
                fireAppearance.DetachParticles();
                Destroy(gameObject, 1);
            }

            fireAppearance.UpdateFireAppearance(fireStrength);

            if (burnFuel) worldTile.DamageTile(fireStrength);
        }

        public float GetFireStrength()
        {
            return fireStrength;
        }

    }
}
