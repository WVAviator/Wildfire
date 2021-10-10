namespace Wildfire
{
    public class HexTileSelectionState : SelectionState
    {
        HexTile activeHexTileSelection;
        public HexTileSelectionState(HexTile hex) : base()
        {
            activeHexTileSelection = hex;
            //Code here should activate upon selecting a new hextile

            //Deselect everything and then select the clicked tile
            foreach (HexTile h in HexTileMap.Instance.GetAllHexTiles())
            {
                if (h.Selection.IsSelected() || h == activeHexTileSelection) h.Selection.ToggleSelection();
            }

            //TODO: Code here for showing a tile information screen


        }

        public override HexTile GetSelectedHexTile()
        {
            return activeHexTileSelection;
        }

        public override void ClickedHexTile(HexTile hex)
        {
            //If it's the same hex, deselect and return to default. Otherwise go to a new instance of this state
            if (hex == activeHexTileSelection)
            {
                //This handles deselection
                SelectionStateManager.SetState(new DefaultSelectionState());
                activeHexTileSelection.Selection.ToggleSelection();
                return;
            }

            SelectionStateManager.SetState(new HexTileSelectionState(hex));
        }

        public override void ClickedUnit(Unit u)
        {
            SelectionStateManager.SetState(new UnitSelectionState(u));
            activeHexTileSelection.Selection.ToggleSelection();
        }
    }
}