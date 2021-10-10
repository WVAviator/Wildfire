using UnityEngine;

namespace Wildfire.TurnActions
{
    public class EngineSuppressantAction : SuppressantAction
    {
        [SerializeField]
        float rotationOffset = -90;

        protected override void ProcessSuppressantApplication(HexTile hex)
        {
            MoveAction moveAction = GetComponent<MoveAction>();
            //Rotate the unit so that the water will apply to the chosen tile
            moveAction.SetTargetRotation(moveAction.GetRotationTowardsTarget(hex.transform.position, rotationOffset));

            base.ProcessSuppressantApplication(hex);
        }

        public override bool CanApplySuppression(HexTile hex)
        {
            //This should only work on a neighbor tile
            if (!Unit.CurrentHex.GetNeighbors().Contains(hex)) return false;

            return base.CanApplySuppression(hex);
        }



    }
}
