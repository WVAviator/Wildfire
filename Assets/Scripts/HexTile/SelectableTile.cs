using UnityEngine;

namespace Wildfire
{
    public class SelectableTile : MonoBehaviour
    {

        Vector3 targetPosition;
        Vector3 startPosition;
        bool isSelected = false;

        [Tooltip("How high should the tile rise up when it is selected?")]
        [SerializeField] float selectionRiseHeight = 0.1f;
        [Tooltip("How quickly should a tile rise upon being selected?")]
        [SerializeField] float selectionRiseSpeed = 1;

        [Header("Selection Material")]
        [SerializeField] Material selectedMaterial;
        [SerializeField] Material deselectedMaterial;

        private void Start()
        {
            Vector3 position = transform.position;
            targetPosition = position;
            startPosition = position;
        }

        public void ToggleSelection()
        {
            isSelected = !isSelected;

            if (isSelected) Select();
            if (!isSelected) Deselect();
        }

        void Select()
        {
            SetMaterial(selectedMaterial);
            targetPosition = new Vector3(targetPosition.x, targetPosition.y + selectionRiseHeight, targetPosition.z);
        }

        void SetMaterial(Material mat)
        {
            transform.GetComponentInChildren<MeshRenderer>().material = mat;
        }

        void Deselect()
        {
            targetPosition = startPosition;
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        private void Update()
        {
            MoveTowardsTarget();
            if (IsAtTarget()) SetMaterial(deselectedMaterial);
        }

        void MoveTowardsTarget()
        {
            if (transform.position != targetPosition) transform.position = Vector3.MoveTowards(transform.position, targetPosition, selectionRiseSpeed * Time.deltaTime);
        }

        bool IsAtTarget()
        {
            return transform.position == startPosition;
        }

    }
}