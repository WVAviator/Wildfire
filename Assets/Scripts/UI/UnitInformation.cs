using UnityEngine;
using UnityEngine.UI;

namespace Wildfire
{
    public class UnitInformation : MonoBehaviour
    {

        [Header("UI Components")]
        public Image unitImage;
        public Text unitText;
        public Image leftArrow;
        public Image rightArrow;
        public Image information;
        public Text movesRemaining;

        private void Start()
        {
            SetUnitDisplay((Unit) null);
            SelectionStateManager.OnEnterSelectionState += SetUnitDisplay;
            Unit.OnTurnActionCompleted += SetUnitDisplay;
        }

        void SetUnitDisplay(SelectionState ss)
        {
            SetUnitDisplay(ss.GetSelectedUnit());
        }

        void SetUnitDisplay(Unit u)
        {
            if (u == null)
            {
                unitImage.enabled = false;
                unitText.text = "";
                movesRemaining.text = "";
                return;
            }

            unitImage.enabled = true;
            unitImage.sprite = u.UnitImage;
            unitText.text = u.UnitName;
            movesRemaining.text = u.GetRemainingTurnActions() + "";
        }
    }
}