namespace Wildfire.TurnActions
{
    public class HelicopterSuppressantAction : SuppressantAction
    {

        public override bool CanApplySuppression(HexTile hex)
        {
            if (hex != Unit.CurrentHex) return false; //TODO: Maybe a message to the player that says you must fly to the hex first before applying water

            return base.CanApplySuppression(hex);
        }

    }
}
