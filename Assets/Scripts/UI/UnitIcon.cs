using UnityEngine;

namespace Wildfire
{
    public class UnitIcon : MonoBehaviour
    {
        [SerializeField] Sprite selected;
        [SerializeField] Sprite unselected;
        [SerializeField] Unit unitParent;


        private void Start()
        {
            SelectionStateManager.OnEnterSelectionState += Highlight;
        }
        void Update()
        {
            Transform iconContainer = transform.parent;
            float newRotationY = -1 * iconContainer.parent.rotation.eulerAngles.y;
        
            Quaternion updatedRotation = Quaternion.Euler(0, newRotationY, 0);
            iconContainer.localRotation = updatedRotation;

        }

        void Highlight(SelectionState ss)
        {
            ShowSelected(ss.GetSelectedUnit() == unitParent);
        }

        void ShowSelected(bool isSelected)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = isSelected ? selected : unselected;
        }
    
    }
}