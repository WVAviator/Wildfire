using UnityEngine;

namespace Wildfire
{
    public class MouseController : MonoBehaviour
    {
        [Header("Zoom Settings")]
        [SerializeField] float minimumZoomDistance = 1.5f;
        [SerializeField] float maximumZoomDistance = 4.5f;
        [SerializeField] float zoomSensitivity = 1;

        [Header("Drag Settings")]
        [SerializeField] float horizontalMapClamp = 0;
        [SerializeField] float southMapClamp = 4;
        [SerializeField] float northMapClamp = -4;
        [SerializeField] float minimumDragDistance = 8;

        [Header("Camera Settings")]
        [SerializeField] int cameraAngle = 50;

        Camera mainCamera;

        Vector3 clickedScreenPoint;
        Vector3 previousYPlaneRayIntersection;
        Vector3 yPlaneRayIntersection;

        bool isDragging;
        public static MouseController Instance;

        [HideInInspector] public bool IsDraggingUIWindow;

        private void Awake()
        {
            Instance = this;
            mainCamera = Camera.main;
            mainCamera.transform.rotation = Quaternion.Euler(cameraAngle, 0, 0);
        }

        private void Update()
        {
            RaycastInformation.SetRay(mainCamera.ScreenPointToRay(Input.mousePosition));
        }

        private void LateUpdate()
        {
            if (RaycastInformation.PointerOverUI()) return;
            if (IsDraggingUIWindow) return;

            HandleClicking();
            HandleDragging();
            HandleZooming();
        }

        private void HandleClicking()
        {
            yPlaneRayIntersection = RaycastInformation.YPlaneRayIntersection;

            if (Input.GetMouseButtonDown(0))
            {
                previousYPlaneRayIntersection = yPlaneRayIntersection;
                clickedScreenPoint = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!isDragging)
                {
                    if (RaycastInformation.OverHex) SelectionStateManager.GetState().ClickedHexTile(RaycastInformation.CurrentHex);
                    if (RaycastInformation.OverUnit) SelectionStateManager.GetState().ClickedUnit(RaycastInformation.CurrentUnit);
                }

                isDragging = false;
            }

            //The player is not considered to be dragging unless they move past the minimum drag distance
            if (Input.GetMouseButton(0)) isDragging = (Input.mousePosition - clickedScreenPoint).sqrMagnitude > (minimumDragDistance * minimumDragDistance);
        }

        private void HandleDragging()
        {
            if (isDragging)
            {
                Vector3 yPlaneRayIntersectionChange = previousYPlaneRayIntersection - yPlaneRayIntersection;
                mainCamera.transform.Translate(yPlaneRayIntersectionChange, Space.World);

                RaycastInformation.SetRay(mainCamera.ScreenPointToRay(Input.mousePosition));
                previousYPlaneRayIntersection = RaycastInformation.YPlaneRayIntersection;

                //The below code uses a lot of extra stuff to determine the edges of the map - could this be done easier with a forward ray that blocks movement past the map bounds?
                //TODO: Try this with a forward ray

                Vector3[] fourCorners = HexTileMap.Instance.GetMapCorners();

                if (mainCamera.transform.position.x < fourCorners[0].x - horizontalMapClamp) mainCamera.transform.position = new Vector3(fourCorners[0].x - horizontalMapClamp, mainCamera.transform.position.y, mainCamera.transform.position.z);
                if (mainCamera.transform.position.x > fourCorners[2].x + horizontalMapClamp) mainCamera.transform.position = new Vector3(fourCorners[2].x + horizontalMapClamp, mainCamera.transform.position.y, mainCamera.transform.position.z);
                if (mainCamera.transform.position.z < fourCorners[0].z - southMapClamp) mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, fourCorners[0].z - southMapClamp);
                if (mainCamera.transform.position.z > fourCorners[2].z + northMapClamp) mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, fourCorners[2].z + northMapClamp);

            }
        }

        private void HandleZooming()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll == 0) return;

            Vector3 position = mainCamera.transform.position;
            Vector3 direction = RaycastInformation.YPlaneRayIntersection - position;
            mainCamera.transform.Translate(direction * scroll * zoomSensitivity, Space.World);

            if (mainCamera.transform.position.y < minimumZoomDistance) mainCamera.transform.position = position;
            if (mainCamera.transform.position.y > maximumZoomDistance) mainCamera.transform.position = position;
        }
    }
}