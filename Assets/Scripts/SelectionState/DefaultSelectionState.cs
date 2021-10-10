namespace Wildfire
{
    public class DefaultSelectionState : SelectionState
    {
        public DefaultSelectionState() : base()
        {
        
        }
    
        public override void ClickedHexTile(HexTile hex)
        {
            //Go to hextile selected state
            SelectionStateManager.SetState(new HexTileSelectionState(hex));

        }

        public override void ClickedUnit(Unit u)
        {
            //Select the unit, open unit info screen and turnaction tray

            SelectionStateManager.SetState(new UnitSelectionState(u));
        }
    }
}