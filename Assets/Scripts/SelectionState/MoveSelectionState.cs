using Wildfire.TurnActions;

namespace Wildfire
{
    public class MoveSelectionState : UnitSelectionState
    {
        MoveAction ma;

        public MoveSelectionState(Unit u) : base(u)
        {
            ma = activeUnitSelection.GetComponent<MoveAction>();
            if (ma == null) SelectionStateManager.SetState(new UnitSelectionState(activeUnitSelection));
            ma.ShowUnselectableTiles();
        }

        public override void ClickedHexTile(HexTile hex)
        {
            //Determine whether it's a legal move
            if (!ma.IsValidSelection(hex)) return;

            ma.Move(hex);

            if (activeUnitSelection.HasAvailableTurnActions()) SelectionStateManager.SetState(new MoveSelectionState(activeUnitSelection));
            else SelectionStateManager.SetState(new DefaultSelectionState());
        }

        public override void ClickedMoveAction()
        {
            SelectionStateManager.SetState(new UnitSelectionState(activeUnitSelection));
        }

    }
}