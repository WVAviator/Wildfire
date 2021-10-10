using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Wildfire.TurnActions
{
    public class SuppressantAction : TurnAction
    {

        [Header("Suppressant Application")]
        [Tooltip("This is the amount by which the suppressant will decrease a tile's fire strength.")]
        [SerializeField] float suppressantEffectiveness = 4;
        [Tooltip("This is the amount by which the suppressant will temporarily reduce a tiles flammability.")]
        [SerializeField] float suppressantFlammabilityReduction = 0.2f;
        [Tooltip("These are the particles that will activate upon water application.")]
        [SerializeField] ParticleSystem[] suppressantParticleSystems;

        public SuppressantType Suppressant;

        bool particlesActive;

        protected override void Start()
        {
            base.Start();
            DisableParticles(suppressantParticleSystems);
        }

        private void DisableParticles(IEnumerable<ParticleSystem> particleSystems)
        {
            foreach (ParticleSystem p in particleSystems) p.Stop();
            particlesActive = false;
        }
        private void EnableParticles(IEnumerable<ParticleSystem> particleSystems)
        {
            foreach (ParticleSystem p in particleSystems) p.Play();
            
            particlesActive = true;
        }

        IEnumerator ActivateParticles(ParticleSystem[] p)
        {
            float startSeconds = Random.Range(0f, 1f);
            float endSeconds = Random.Range(4f, 6f);

            yield return new WaitForSeconds(startSeconds);
            EnableParticles(p);
            yield return new WaitForSeconds(endSeconds);
            DisableParticles(p);
            yield return null;
        }

        IEnumerator SuppressFire(float fireSuppression, HexTile hex)
        {
            yield return new WaitForSeconds(5);

            if (hex.GetComponentInChildren<Fire>() != null)
                hex.GetComponentInChildren<Fire>().UpdateFireStrength(-fireSuppression, false);

            yield return null;
        }

        public virtual void ApplySuppression(HexTile hex)
        {
            if (!CanApplySuppression(hex)) return;

            ProcessSuppressantApplication(hex);
            
            Unit.ExecuteTurnAction();
        }

        public virtual bool CanApplySuppression(HexTile hex)
        {
            if (SelectionStateManager.GetState().GetSelectedUnit() != Unit) return false;

            if (particlesActive) return false; //Don't let the player start another water animation before the last one is finished

            if (!Unit.HasAvailableTurnActions()) return false;

            return true;
        }

        protected virtual void ProcessSuppressantApplication(HexTile hex)
        {
            StartCoroutine(ActivateParticles(suppressantParticleSystems));
            
            StartCoroutine(SuppressFire(suppressantEffectiveness, hex));
        }

        public enum SuppressantType
        {
            Water, Foam, Retardant
        }
    }
}
