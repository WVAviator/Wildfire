using UnityEngine;
using UnityEngine.EventSystems;

namespace Wildfire
{
    public class UIElementDragger : EventTrigger
    {
        bool dragging;
        Vector2 offset;
        public void Update()
        {
            if (dragging)
            {
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            dragging = true;
            MouseController.Instance.IsDraggingUIWindow = true;
            offset = eventData.position - (Vector2)transform.position;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            dragging = false;
            MouseController.Instance.IsDraggingUIWindow = false;
        }
    }
}