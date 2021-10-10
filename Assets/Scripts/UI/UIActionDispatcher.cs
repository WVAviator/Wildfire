using UnityEngine;

namespace Wildfire
{
    /// <summary>
    /// Repository of public functions for reference by UI elements
    /// </summary>
    public class UIActionDispatcher : MonoBehaviour
    {
        public void ActivateMoveAction() => SelectionStateManager.GetState().ClickedMoveAction();
        public void ActivateWaterApplication() => SelectionStateManager.GetState().ClickedWaterSuppressantAction();
        public void ActivateFoamApplication() => SelectionStateManager.GetState().ClickedFoamSuppressantAction();
        public void ActivateRetardantApplication() => SelectionStateManager.GetState().ClickedRetardantSuppressantAction();
        public void ActivateFireline() => SelectionStateManager.GetState().ClickedFirelineAction();
        public void ActivateFuelReductionAction() => SelectionStateManager.GetState().ClickedFuelReductionAction();
        public void ActivateCommandPostConstruction() => SelectionStateManager.GetState().ClickedCommandPostConstructionAction();
        public void ActivateFireIgnition() => SelectionStateManager.GetState().ClickedIgniteFireAction();
        public void ActivateDropCrewAction() => SelectionStateManager.GetState().ClickedDropCrewAction();

        public void ActivateNextUnit() => Unit.SelectNextUnit();
        public void ActivatePreviousUnit() => Unit.SelectPreviousUnit();


    
    }
}