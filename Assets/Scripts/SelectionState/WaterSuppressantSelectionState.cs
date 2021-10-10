using System.Linq;
using Wildfire.TurnActions;

namespace Wildfire
{
    public class WaterSuppressantSelectionState : UnitSelectionState
    {
        SuppressantAction sa;

        public WaterSuppressantSelectionState(Unit u) : base(u)
        {
            sa = activeUnitSelection.GetComponents<SuppressantAction>().FirstOrDefault(s => s.Suppressant == SuppressantAction.SuppressantType.Water);
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

        public override void ClickedWaterSuppressantAction()
        {
            SelectionStateManager.SetState(new UnitSelectionState(activeUnitSelection));
        }
    }
}