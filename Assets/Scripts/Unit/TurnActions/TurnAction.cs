using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Unit))]
namespace Wildfire.TurnActions
{
    public abstract class TurnAction : MonoBehaviour
    {
        protected Unit Unit;

        [SerializeField] int turnActionRange = 1;

        protected virtual void Start()
        {
            Unit = GetComponent<Unit>();
        }

        public virtual bool IsValidSelection(HexTile hex)
        {
            List<HexTile> tiles = HexTileMap.Instance.GetHexTilesInRange(Unit.CurrentHex, turnActionRange);
            tiles.Remove(Unit.CurrentHex);
            return tiles.Contains(hex);
        }

        public virtual void ShowUnselectableTiles()
        {
            List<HexTile> selectables = HexTileMap.Instance.GetHexTilesInRange(Unit.CurrentHex, turnActionRange);
            HexTileMap.Instance.ShroudUnselectables(selectables);
        }

    }
}
