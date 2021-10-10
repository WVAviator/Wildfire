using System.Linq;
using Wildfire.TurnActions;

namespace Wildfire
{
    public class FoamSuppressantSelectionState : UnitSelectionState
    {
        SuppressantAction sa;

        public FoamSuppressantSelectionState(Unit u) : base(u)
        {
            sa = activeUnitSelection.GetComponents<SuppressantAction>().FirstOrDefault(s => s.Suppressant == SuppressantAction.SuppressantType.Foam);
            if (sa == null) SelectionStateManager.SetState(new UnitSelectionState(activeUnitSelection));

            sa.ShowUnselectableTiles();
        }

        public override void ClickedHexTile(HexTile hex)
        {
            if (!sa.CanApplySuppression(hex)) return;
            sa.ApplySuppression(hex);

            if (activeUnitSelection.HasAvailableTurnActions()) return;
            SelectionStateManager.SetState(new UnitSelectionState(activeUnitSelection));
        }

        public override void ClickedFoamSuppressantAction()
        {
            SelectionStateManager.SetState(new UnitSelectionState(activeUnitSelection));
        }
    }
}