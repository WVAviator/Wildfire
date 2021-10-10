namespace Wildfire
{
    public abstract class SelectionState
    {
        protected SelectionState()
        {
            HexTileMap.Instance.UnShroudAllTiles();
        }

        public virtual void ClickedHexTile(HexTile hex) { }
        public virtual void ClickedUnit(Unit u) { }
        public virtual void ClickedMoveAction() { }
        public virtual void ClickedWaterSuppressantAction() { }
        public virtual void ClickedFoamSuppressantAction() { }
        public virtual void ClickedRetardantSuppressantAction() { }
        public virtual void ClickedFirelineAction() { }
        public virtual void ClickedFuelReductionAction() { }
        public virtual void ClickedCommandPostConstructionAction() { }
        public virtual void ClickedIgniteFireAction() { }
        public virtual void ClickedDropCrewAction() { }

        public virtual HexTile GetSelectedHexTile() { return null; }

        public virtual Unit GetSelectedUnit() { return null; }

    }
}