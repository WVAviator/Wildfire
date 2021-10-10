using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wildfire
{
    public static class RaycastInformation
    {
        //This is going to be a class used to distribute information about the player's input in this current frame
        //The information in this class should be updated every frame

        /// <summary>
        /// The Mouse Ray is a ray from the screen to the world.
        /// </summary>
        public static Ray MouseRay;

        /// <summary>
        /// The hit position is the point along the y-coordinate plane along which the player's ray intersects.
        /// </summary>
        public static Vector3 YPlaneRayIntersection;

        /// <summary>
        /// This returns true if the mouse ray intersects with a collider in the world.
        /// </summary>
        public static bool RaycastCollision;

        /// <summary>
        /// This provides information about a collider intersecting with the player's mouse ray.
        /// </summary>
        public static RaycastHit HitInfo;

        /// <summary>
        /// Returns the HexTile currently intersecting the mouse ray. Returns null if no HexTile is selected.
        /// </summary>
        public static HexTile CurrentHex;

        /// <summary>
        /// Returns true if the pointer is over a hextile.
        /// </summary>
        public static bool OverHex;

        /// <summary>
        /// Returns true if the pointer is over a unit icon.
        /// </summary>
        public static bool OverUnit;

        /// <summary>
        /// Returns the Unit currently intersecting the mouse ray. Returns null if no Unit is selected.
        /// </summary>
        public static Unit CurrentUnit;


        public static bool PointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        public static event Action<HexTile> OnHoverOverHex = delegate { };
        public static event Action<Unit> OnHoverOverUnit = delegate { };

        /// <summary>
        /// This method repopulates InputInformation with all the information needed. Should be updated every frame.
        /// </summary>
        /// <param name="ray"></param>
        public static void SetRay(Ray ray)
        {
        
            MouseRay = ray;

            YPlaneRayIntersection = GetYIntersection(ray);

            RaycastCollision = Physics.Raycast(ray, out HitInfo);

            CurrentHex = null;
            CurrentUnit = null;
            OverHex = false;
            OverUnit = false;

            if (!RaycastCollision) return;

            Transform parent = HitInfo.transform.parent.parent;
            OverHex = parent.TryGetComponent<HexTile>(out CurrentHex);
            OverUnit = parent.TryGetComponent<Unit>(out CurrentUnit);

            if (OverHex) OnHoverOverHex?.Invoke(CurrentHex);
            if (OverUnit) OnHoverOverUnit?.Invoke(CurrentUnit);
        
        }

        static Vector3 GetYIntersection(Ray ray)
        {
            //This is used to calculate y plane ray intersection
            float length = ray.origin.y / ray.direction.y;
            return ray.origin - (ray.direction * length);
        }
    }
}