using Wildfire.TurnActions;

namespace Wildfire
{
    public class UnitSelectionState : SelectionState
    {
        protected Unit activeUnitSelection;

        public UnitSelectionState(Unit u) : base()
        {
            activeUnitSelection = u;
        }

        public override Unit GetSelectedUnit()
        {
            return activeUnitSelection;
        }

        public override void ClickedHexTile(HexTile hex)
        {
            SelectionStateManager.SetState(new HexTileSelectionState(hex));
        }

        public override void ClickedUnit(Unit u)
        {
            if (activeUnitSelection == u)
            {
                SelectionStateManager.SetState(new DefaultSelectionState());
                return;
            }
            SelectionStateManager.SetState(new UnitSelectionState(u));
        }

        public override void ClickedMoveAction()
        {
            if (!activeUnitSelection.HasAvailableTurnActions()) return;
        
            if (activeUnitSelection.GetComponent<MoveAction>() == null) return;

            SelectionStateManager.SetState(new MoveSelectionState(activeUnitSelection));
        }

        public override void ClickedWaterSuppressantAction()
        {
            if (!activeUnitSelection.HasAvailableTurnActions()) return;
            SelectionStateManager.SetState(new WaterSuppressantSelectionState(activeUnitSelection));
        }
        
        public override void ClickedFoamSuppressantAction()
        {
            if (!activeUnitSelection.HasAvailableTurnActions()) return;
            SelectionStateManager.SetState(new FoamSuppressantSelectionState(activeUnitSelection));
        }

    }
}