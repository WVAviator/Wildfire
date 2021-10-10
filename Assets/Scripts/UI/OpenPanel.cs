using UnityEngine;

namespace Wildfire
{
    public class OpenPanel : MonoBehaviour
    {
        [SerializeField] Transform openArrow;
        [SerializeField] float panelSpeed = 3f;

        bool isOpen;
        Vector2 targetPosition;
        Vector2 closedPosition = new Vector2(-450, 0);
        Vector2 openPosition = new Vector2(0, 0);

        RectTransform rt;

        private void Start()
        {
            targetPosition = closedPosition;
            isOpen = false;
            rt = GetComponent<RectTransform>();

            SelectionStateManager.OnEnterSelectionState += DisplayUnit;
        }

        public void TogglePanel()
        {
            if (isOpen) CollapsePanel();
            else ExpandPanel();
        }

        void ExpandPanel()
        {
            if (isOpen) return;
            targetPosition = openPosition;
            openArrow.rotation = Quaternion.Euler(0, 0, 90);
            isOpen = true;
        }

        void CollapsePanel()
        {
            if (!isOpen) return;
            targetPosition = closedPosition;
            openArrow.rotation = Quaternion.Euler(0, 0, -90);
            isOpen = false;
        }

        void Update()
        {
            if (rt.anchoredPosition != targetPosition)
            {
                rt.anchoredPosition =
                    Vector3.MoveTowards(rt.anchoredPosition, targetPosition, panelSpeed * Time.deltaTime);
            }
        }
        void DisplayUnit(SelectionState ss)
        {
            DisplayUnit(ss.GetSelectedUnit());
        }
        void DisplayUnit(Unit u)
        {
            if (u == null) CollapsePanel();
            if (u != null) ExpandPanel();
        }
    }
}