using UnityEngine;

namespace Wildfire
{
    public class HoverShroud : MonoBehaviour
    {
        [Tooltip("The hover shroud material.")]
        [SerializeField] Material hoverShroudMaterial;

        [Tooltip("The dark shroud material.")]
        [SerializeField] Material darkShroudMaterial;

        [Tooltip("The shroud height raises or lowers the shroud as compared to the visible hex mesh.")]
        [SerializeField] float shroudHeight = 0.1f;

        [Tooltip("Shroud inset decreases the width of the shroud as compared to the visible hex mesh.")]
        [SerializeField] float shroudInset = 3;

        GameObject shroud;
        GameObject darkShroud;
        HexTile parentHex;

        private void Start()
        {
            parentHex = transform.parent.GetComponent<HexTile>();
            RaycastInformation.OnHoverOverHex += HoverOverTile;
        }
        public void InitializeShroud(GameObject hexagon)
        {
            if (shroud != null) return;
            
            Vector3 position = hexagon.transform.position;
            Vector3 newPosition = new Vector3(position.x, position.y + shroudHeight, position.z);

            shroud = Instantiate(hexagon, newPosition, hexagon.transform.rotation, transform);

            shroud.transform.localScale = new Vector3(100 - shroudInset, 100 - shroudInset, 100);

            shroud.GetComponent<MeshRenderer>().material = hoverShroudMaterial;

            InitializeDarkShroud();

            shroud.SetActive(false);
        }

        void HoverOverTile(HexTile hex)
        {
            if (darkShroud.activeSelf) return;

            if (hex == parentHex)
            {
                if (shroud.activeSelf) return;
                shroud.SetActive(true);
            }
            else
            {
                if (shroud.activeSelf) shroud.SetActive(false);
            }
        }

        void InitializeDarkShroud()
        {
            darkShroud = Instantiate(shroud, shroud.transform.position, shroud.transform.rotation, shroud.transform.parent);
            darkShroud.GetComponent<MeshRenderer>().material = darkShroudMaterial;
            darkShroud.SetActive(false);
        }

        public void ShowTileUnselectable()
        {
            if (!darkShroud.activeSelf) darkShroud.SetActive(true);
        }

        public void ShowTileSelectable()
        {
            if (darkShroud.activeSelf) darkShroud.SetActive(false);
        }

    }
}