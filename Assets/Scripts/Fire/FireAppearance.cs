using System.Collections.Generic;
using UnityEngine;

namespace Wildfire
{
    public class FireAppearance : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem treeFirePrefab;
        [SerializeField]
        ParticleSystem brushFirePrefab;
        [SerializeField]
        ParticleSystem smokePrefab;

        [SerializeField]
        int brushFireDensity = 10;

        List<ParticleSystem> treeFires;
        List<ParticleSystem> brushFires;
        ParticleSystem smoke;

        List<Vector3> possibleBrushLocations;
        List<Vector3> treeLocations;

        HexTile hex;
        HexTileInfo hexInfo;

        float maxFireStrength;

        void Awake()
        {

            hex = transform.parent.GetComponent<HexTile>();
            hexInfo = hex.GetComponent<HexTileInfo>();
            maxFireStrength = Fire.GetMaxFireStrength();

            treeLocations = new List<Vector3>();
            foreach (Tree tree in transform.parent.GetComponentInChildren<ProceduralForestTile>().GetComponentsInChildren<Tree>()) treeLocations.Add(tree.transform.position);
            possibleBrushLocations = transform.parent.GetComponentInChildren<ProceduralTileInfo>().GetWorldCoordinates();

            InstantiateSmoke();
            InstantiateBrushFires();
            InstantiateTreeFires();

            DisableAllParticles();


        }

        private void InstantiateTreeFires()
        {
            treeFires = new List<ParticleSystem>();
            foreach (Vector3 v in treeLocations)
            {
                ParticleSystem p = Instantiate(treeFirePrefab, v, Quaternion.Euler(-90, 0, 0), transform);
                treeFires.Add(p);
            }
        }

        private void InstantiateBrushFires()
        {
            brushFires = new List<ParticleSystem>();
            possibleBrushLocations.Shuffle();
            for (int i = 0; i < brushFireDensity; i++)
            {
                ParticleSystem p = Instantiate(brushFirePrefab, possibleBrushLocations[i], Quaternion.Euler(-90, 0, 0), transform);
                brushFires.Add(p);
            }
        }

        private void InstantiateSmoke()
        {
            ParticleSystem p = Instantiate(smokePrefab, hexInfo.GetCenterOfTile(), Quaternion.Euler(-90, 0, 0), transform);
            smoke = p;
        }

        void DisableAllParticles()
        {
            DisableParticles(treeFires);
            DisableParticles(brushFires);
            DisableParticles(smoke);
        }
        public void UpdateFireAppearance(float fireStrength)
        {
            if (fireStrength <= 0)
            {
                DisableAllParticles();
                return;
            }
        
            SpawnSmoke();
            SpawnBrushFire(fireStrength);
            UpdateTreeFires(fireStrength);
        
        
        }

        private void UpdateTreeFires(float fireStrength)
        {
            if (fireStrength < (maxFireStrength * 0.5f))
            {
                DisableParticles(treeFires);
                return;
            }
            float fireSize = (fireStrength - (maxFireStrength / 2)) / (maxFireStrength / 2);
            foreach (ParticleSystem tf in treeFires)
            {
                EnableParticles(treeFires);

                ParticleSystem.MainModule psmain = tf.main;
                psmain.startLifetime = (fireSize * 5) + 5;

                ParticleSystem.EmissionModule psemit = tf.emission;
                psemit.rateOverTime = (fireSize * 10) + 10;
            }
        }

        private void DisableParticles(List<ParticleSystem> systems)
        {
            foreach (ParticleSystem ps in systems) ps.Stop();
        }
        private void DisableParticles(ParticleSystem system)
        {
            system.Stop();
        }

        private void EnableParticles(List<ParticleSystem> systems)
        {
            foreach (ParticleSystem ps in systems) ps.Play();
        }
        private void EnableParticles(ParticleSystem system)
        {
            system.Play();
        }



        private void SpawnBrushFire(float fireStrength)
        {
            if (fireStrength < maxFireStrength * 0.25f)
            {
                DisableParticles(brushFires);
                return;
            }
            EnableParticles(brushFires);
        }

        void SpawnSmoke()
        {
            EnableParticles(smoke);
        }

        public void DetachParticles()
        {
            DisableAllParticles();
            foreach (ParticleSystem ps in brushFires)
            {
                ps.transform.parent = null;
                Destroy(ps.gameObject, 3);
            }
            foreach (ParticleSystem ps in treeFires)
            {
                ps.transform.parent = null;
                Destroy(ps.gameObject, 3);
            }
            smoke.transform.parent = null;
            Destroy(smoke.gameObject, 15);
        }
    }
}
