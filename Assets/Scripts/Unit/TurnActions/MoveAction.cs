using UnityEngine;

namespace Wildfire.TurnActions
{
    public class MoveAction : TurnAction
    {

        [Header("Movement")]
        [Tooltip("The speed that a unit will glide from one tile to another when moved.")]
        public float unitSpeed = 1f;
        [Tooltip("The speed at which a unit will turn towards the destination tile")]
        public float rotationSpeed = 10f;
        [Tooltip("The offset direction the unit is facing.")]
        public float angleOffset = 0f;


        protected Vector3 destination;
        protected Quaternion targetRotation;
        protected TerrainHugger[] terrainHuggers;
    

        protected override void Start()
        {
            base.Start();

            //Initialize destination with current position so the unit doesn't try to go anywhere.
            destination = transform.position;
            targetRotation = transform.rotation;

            terrainHuggers = Unit.GetComponentsInChildren<TerrainHugger>();

        }

        protected void Update()
        {
            RotateTowardsTargetRotation();
            MoveTowardsDestination();
        }

        protected virtual void RotateTowardsTargetRotation()
        {
            if (transform.rotation.eulerAngles == targetRotation.eulerAngles) return;
            RotateTowards(targetRotation);
            UpdateTerrainHuggers();
        }

        void UpdateTerrainHuggers()
        {
            if (terrainHuggers.Length != 0)
            {
                foreach (TerrainHugger th in terrainHuggers) th.HugTerrain();
            }
        }

        public Quaternion GetRotationTowardsTarget(Vector3 target, float additionalOffset = 0)
        {
            Vector3 direction = (target - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y + angleOffset + additionalOffset, 0);

            return lookRotation;
        }

        protected virtual void RotateTowards(Quaternion rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        protected virtual void MoveTowardsDestination()
        {
            //If the destination is different than the current position, move towards the destination (excluding the y coordinate)
            if (!Arrived())
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, unitSpeed * Time.deltaTime);
                UpdateTerrainHuggers();
            }

            //Once the destination is reached, reparent the unit to the hex it is on
            //Can't use bool Arrived() here because the y coordinate needs to be included
            if (Unit.CurrentHex.transform.position == transform.position && transform.parent == null) transform.parent = Unit.CurrentHex.transform;
        }

        public void SetTargetRotation(Quaternion rotation)
        {
            targetRotation = rotation;
        }

        protected bool Arrived()
        {
            return destination.x == transform.position.x && destination.z == transform.position.z;
        }

        public void Move(HexTile hex)
        {
        
            //This should never happen with selection modes but if it does just return out of the method
            if (SelectionStateManager.GetState().GetSelectedUnit() != Unit) return;

            //Ideally this shouldn't be necessary either if unit is no longer selectable after running out of moves
            if (!Unit.HasAvailableTurnActions()) return;

            //Check that the selected tile is a neighbor
            //TODO: Ideally we'd also like to be able to make two moves in one click, so this may need revision
            if (!IsValidSelection(hex)) return;

            //Here we start the actual move process

            //Deparent from the hex
            transform.parent = null;

            //The destination coordinates are the coordinates of the destination hex
            destination = hex.GetXZWorldCoordinates();

            //Set the target rotation towards the destination
            targetRotation = GetRotationTowardsTarget(destination);

            //Set the new current hex
            Unit.CurrentHex = hex;

            //Decrement moves remaining
            Unit.ExecuteTurnAction();

            //If there are no remaining moves, deselect
            //if (!unit.HasAvailableTurnActions()) UnitManager.Instance.SetSelectedUnit(null);

            //The rest of the move action occurs in the update method


        }

    }
}
