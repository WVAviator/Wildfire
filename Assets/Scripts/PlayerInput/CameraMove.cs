using UnityEngine;

namespace Wildfire
{
    /// <summary>
    /// CameraMove allows the camera to move towards a specified point. This is great for selecting units from the UI.
    /// </summary>
    public class CameraMove : MonoBehaviour
    {
        [Tooltip("The time it takes for the camera to travel the specified distance.")]
        [SerializeField] private float moveSpeed = 0.1f;
    
        [Tooltip("The y-Height of the camera following an automove.")]
        [SerializeField] private float yOffset = 3.6f;
    
        [Tooltip("The z-Offset of the camera following an automove.")]
        [SerializeField] private float zOffset = 3f;

        Vector3 targetPosition;
        bool autoMove;

        private void Start()
        {
            SelectionStateManager.OnNewUnitSelection += MoveTowards;
        }
        void Update()
        {
            ExecuteMove();

            if (Input.GetMouseButtonDown(0)) CancelMove();
        }

        void ExecuteMove()
        {
            //Automove has to be true for any movement to occur. This allows the movement to be cancelled if desired.
            if (autoMove && targetPosition != transform.position)
            {
                Vector3 currentVelocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, moveSpeed);
            }

            if (targetPosition == transform.position) autoMove = false;
        }
        void MoveTowards(Unit u)
        {
            if (u == null) return;
        
            autoMove = true;

            Vector3 position = u.transform.position;
            Vector3 newPos = new Vector3(position.x, yOffset, position.z - zOffset);

            targetPosition = newPos;
        }

        void CancelMove()
        {
            autoMove = false;
        }
    }
}
